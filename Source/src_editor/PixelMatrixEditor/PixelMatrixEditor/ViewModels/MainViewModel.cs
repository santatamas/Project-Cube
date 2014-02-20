using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Controls;
using System.Windows.Forms;
using CubeProject.Data.Entities;
using CubeProject.Data.Serializers;
using Microsoft.Practices.Prism.Commands;
using PixelMatrixEditor.Annotations;
using Application = System.Windows.Application;
using ColorDepth = CubeProject.Infrastructure.Enums.ColorDepth;
using MessageBox = System.Windows.MessageBox;

namespace PixelMatrixEditor.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Construction
        public MainViewModel()
        {
            SelectedAreaSize = 1;
            _animation = new Animation
            {
                ColorDepth = ColorDepth.Onebit
            };

            for (int i = 0; i < 30; i++)
            {
                _animation.Frames.Add(new Frame<byte>(50,50));
                _animation.FrameDurations.Add(500);
            }
            _currentMatrix = _animation.Frames[0];
        }
        #endregion

        #region Properties

        public string ActiveStatusMessage
        {
            get { return _activeStatusMessage; }
            set
            {
                if (value == _activeStatusMessage) return;
                _activeStatusMessage = value;
                OnPropertyChanged();
            }
        }

        public string CurrentFilePath { get; set; }

        public int SelectedAreaSize
        {
            get { return _selectedAreaSize; }
            set
            {
                if (value == _selectedAreaSize) return;
                _selectedAreaSize = value;
                OnPropertyChanged();
            }
        }

        public List<int> AreaSize
        {
            get
            {
                return new List<int> { 1, 2, 3, 4 };
            }
        }

        public Frame<byte> CurrentMatrix
        {
            get { return _currentMatrix; }
            set
            {
                if (Equals(value, _currentMatrix)) return;
                _currentMatrix = value;
                OnPropertyChanged();
            }
        }

        public Animation Animation
        {
            get { return _animation; }
            set
            {
                if (Equals(value, _animation)) return;
                _animation = value;

                if(_animation != null && _animation.Frames.Count != 0)
                    CurrentMatrix = _animation.Frames[0];

                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands
        public DelegateCommand<object> NewCommand
        {
            get { return _newCommand ?? (_newCommand = new DelegateCommand<object>(CreateNew)); }
        }
        public DelegateCommand<object> OpenCommand
        {
            get { return _openCommand ?? (_openCommand = new DelegateCommand<object>(Open)); }
        }
        public DelegateCommand<object> SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new DelegateCommand<object>(Save)); }
        }
        public DelegateCommand<object> SaveAsCommand
        {
            get { return _saveAsCommand ?? (_saveAsCommand = new DelegateCommand<object>(SaveAs)); }
        }
        public DelegateCommand<object> CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new DelegateCommand<object>(CloseApplication)); }
        }

        #endregion

        #region Private
        private void CloseApplication(object obj)
        {
            Application.Current.Shutdown();
        }

        private void CreateNew(object obj)
        {
            Animation = new Animation();
            ActiveStatusMessage = "New animation created.";
        }

        private void Open(object obj)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            // set a default file name
            ofd.FileName = "unknown.pma";
            // set filters - this can be done in properties as well
            ofd.Filter = "Pixel Matrix Animation (*.pma)|*.pma|All files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var serializer = new AnimationSerializer();
                Animation = serializer.Deserialize(File.Open(ofd.FileName, FileMode.Open));
                CurrentFilePath = ofd.FileName;
                ActiveStatusMessage = "Animation loaded.";
            }
        }

        private void Save(object obj)
        {
            if (!String.IsNullOrEmpty(CurrentFilePath))
            {
                SaveAnimationTo(Animation, CurrentFilePath);
                ActiveStatusMessage = "Saving complete.";
            }
        }

        private void SaveAs(object obj)
        {
            var sfd = new SaveFileDialog();
            // set a default file name
            sfd.FileName = "unknown.pma";
            // set filters - this can be done in properties as well
            sfd.Filter = "Pixel Matrix Animation (*.pma)|*.pma|All files (*.*)|*.*";

            if (sfd.ShowDialog() != DialogResult.OK) return;

            SaveAnimationTo(Animation, sfd.FileName);
            CurrentFilePath = sfd.FileName;
            ActiveStatusMessage = "Saving complete.";
        }

        private void SaveAnimationTo(Animation animation, string path)
        {
            var serializer = new AnimationSerializer();
            using (var animStream = serializer.Serialize(animation))
            {
                animStream.Position = 0;

                using (var fs = File.Create(path))
                {
                    animStream.CopyTo(fs);
                }
            }
        }

        #region Private State
        private int _selectedAreaSize;
        private DelegateCommand<object> _newCommand;
        private DelegateCommand<object> _openCommand;
        private DelegateCommand<object> _saveCommand;
        private DelegateCommand<object> _saveAsCommand;
        private DelegateCommand<object> _closeCommand;
        private Frame<byte> _currentMatrix;
        private Animation _animation;
        private string _activeStatusMessage;

        #endregion
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
