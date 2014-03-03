using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using CubeProject.Data.Entities;
using CubeProject.Data.Serializers;
using CubeProject.Infrastructure.BaseClasses;
using CubeProject.Infrastructure.Enums;
using CubeProject.Infrastructure.Events;
using CubeProject.Infrastructure.Interfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

namespace CubeProject.Modules.Editor.ViewModels
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        #region Construction
        public MainViewModel(IUnityContainer container, IEventAggregator aggregator)
            : base(container, aggregator)
        {
            EventAggregator.GetEvent<CloseApplicationEvent>().Subscribe(CloseApplication);

            _animation = CreateNewAnimation(ColorDepth.GrayScale, 50, 50);
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

        public string CurrentFilePath { get; set; }


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

                if (_animation != null)
                {
                    if (_animation.Frames.Count != 0)
                    {
                        CurrentMatrix = _animation.Frames[0];
                    }

                    if (_animation.ColorDepth == ColorDepth.Onebit)
                    {
                        //SelectedShadeLevel = 1;
                    }
                }

                OnPropertyChanged();
            }
        }



        #endregion

        #region Commands

        public DelegateCommand<object> AddFrameCommand
        {
            get { return _addFrameCommand ?? (_addFrameCommand = new DelegateCommand<object>(AddFrame)); }
        }

        #endregion

        #region Private
        private void CloseApplication(string param)
        {
            Application.Current.Shutdown();
        }

        private void CreateNew(object obj)
        {
            //NewAnimationView newAnimWindow = new NewAnimationView();
            //if (newAnimWindow.ShowDialog() == true)
            //{
            //    var viewModel = (newAnimWindow.DataContext as NewAnimationViewModel);
            //    Animation = CreateNewAnimation(viewModel.SelectedColorDepth, viewModel.FrameWidth, viewModel.FrameHeight);
            //    ActiveStatusMessage = "New animation created.";

            //}
        }

        private void Open(object obj)
        {
            //OpenFileDialog ofd = new OpenFileDialog();
            //// set a default file name
            //ofd.FileName = "unknown.pma";
            //// set filters - this can be done in properties as well
            //ofd.Filter = "Pixel Matrix Animation (*.pma)|*.pma|All files (*.*)|*.*";

            //if (ofd.ShowDialog() == DialogResult.OK)
            //{
            //    var serializer = new AnimationSerializer();
            //    Animation = serializer.Deserialize(File.Open(ofd.FileName, FileMode.Open));
            //    CurrentFilePath = ofd.FileName;
            //    ActiveStatusMessage = "Animation loaded.";
            //}
        }

        private void Save(object obj)
        {
            if (!String.IsNullOrEmpty(CurrentFilePath))
            {
                SaveAnimationTo(Animation, CurrentFilePath);
                //ActiveStatusMessage = "Saving complete.";
            }
        }

        private void SaveAs(object obj)
        {
            //var sfd = new SaveFileDialog();
            //// set a default file name
            //sfd.FileName = "unknown.pma";
            //// set filters - this can be done in properties as well
            //sfd.Filter = "Pixel Matrix Animation (*.pma)|*.pma|All files (*.*)|*.*";

            //if (sfd.ShowDialog() != DialogResult.OK) return;

            //SaveAnimationTo(Animation, sfd.FileName);
            //CurrentFilePath = sfd.FileName;
            //ActiveStatusMessage = "Saving complete.";
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
            //MessageBoxButtons btnMessageBox = MessageBoxButtons.YesNo;
            //MessageBoxIcon icnMessageBox = MessageBoxIcon.Warning;

            //MessageBoxResult rsltMessageBox = (MessageBoxResult)MessageBox.Show("Are you sure you want to delete the Frame?", "Delete", btnMessageBox, icnMessageBox);

            //switch (rsltMessageBox)
            //{
            //    case MessageBoxResult.Yes:
            //        Animation.Frames.Remove((Frame<byte>)obj);
            //        break;

            //    case MessageBoxResult.No:
            //        /* ... */
            //        break;
            //}
        }

        private void AddFrame(object obj)
        {
            Animation.Frames.Add(new Frame<byte>(_currentAnimationFrameWidth, _currentAnimationFrameHeight));
        }

        #region Private State

        private Frame<byte> _currentMatrix;
        private Animation _animation;

        private DelegateCommand<object> _addFrameCommand;

        private short _currentAnimationFrameWidth = 0;
        private short _currentAnimationFrameHeight = 0;


        #endregion

        #endregion
    }
}
