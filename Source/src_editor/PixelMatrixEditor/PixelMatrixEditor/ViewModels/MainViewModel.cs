using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;
using CubeProject.Data.Entities;
using CubeProject.Data.Serializers;
using CubeProject.Infrastructure.BaseClasses;
using Microsoft.Practices.Prism.Commands;
using PixelMatrixEditor.Annotations;
using PixelMatrixEditor.Views;
using Application = System.Windows.Application;
using ColorDepth = CubeProject.Infrastructure.Enums.ColorDepth;
using MessageBox = System.Windows.Forms.MessageBox;

namespace PixelMatrixEditor.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Construction
        public MainViewModel()
        {
            SelectedAreaSize = 1;
           _animation = CreateNewAnimation(ColorDepth.Onebit, 50,50);
           _currentMatrix = _animation.Frames[0];
        }

        private Animation CreateNewAnimation(ColorDepth depth, short frameWidth, short frameHeight)
        {
            var animation = new Animation()
            {
                ColorDepth = depth
            };

            for (int i = 0; i < 5; i++)
            {
                animation.Frames.Add(new Frame<byte>(frameWidth, frameHeight));
                animation.FrameDurations.Add(500);
            }

            _currentAnimationFrameWidth = frameWidth;
            _currentAnimationFrameHeight = frameHeight;

            return animation;
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

        public DelegateCommand<object> DeleteFrameCommand
        {
            get { return _deleteFrameCommand ?? (_deleteFrameCommand = new DelegateCommand<object>(DeleteFrame)); }
        }

        public DelegateCommand<object> AddFrameCommand
        {
            get { return _addFrameCommand ?? (_addFrameCommand = new DelegateCommand<object>(AddFrame)); }
        }

        #endregion

        #region Private
        private void CloseApplication(object obj)
        {
            Application.Current.Shutdown();
        }

        private void CreateNew(object obj)
        {
            NewAnimationView newAnimWindow = new NewAnimationView();
            if (newAnimWindow.ShowDialog() == true)
            {
                var viewModel = (newAnimWindow.DataContext as NewAnimationViewModel);
                Animation = CreateNewAnimation(viewModel.SelectedColorDepth, viewModel.FrameWidth, viewModel.FrameHeight);
                ActiveStatusMessage = "New animation created.";

            }
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
        private void DeleteFrame(object obj)
        {
            MessageBoxButtons btnMessageBox = MessageBoxButtons.YesNo;
            MessageBoxIcon icnMessageBox = MessageBoxIcon.Warning;

            MessageBoxResult rsltMessageBox = (MessageBoxResult)MessageBox.Show("Are you sure you want to delete the Frame?", "Delete", btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    Animation.Frames.Remove((Frame<byte>)obj);
                    break;

                case MessageBoxResult.No:
                    /* ... */
                    break;
            }
        }

        private void AddFrame(object obj)
        {
            Animation.Frames.Add(new Frame<byte>(_currentAnimationFrameWidth, _currentAnimationFrameHeight));
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
        private DelegateCommand<object> _deleteFrameCommand;
        private DelegateCommand<object> _addFrameCommand;

        private short _currentAnimationFrameWidth = 0;
        private short _currentAnimationFrameHeight = 0;

        #endregion

        #endregion
    }
}
