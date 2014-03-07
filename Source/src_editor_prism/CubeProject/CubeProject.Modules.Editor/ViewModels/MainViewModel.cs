using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
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
            _dialogService = container.Resolve<IDialogService>();
            FrameViewModels = new ObservableCollection<FrameViewModel>();

            SubscribeEvents();

            Animation = CreateNewAnimation(ColorDepth.GrayScale, 50, 50);
        }

        private void SubscribeEvents()
        {
            EventAggregator.GetEvent<CloseApplicationEvent>().Subscribe(CloseApplication);
            EventAggregator.GetEvent<CreateNewAnimationEvent>().Subscribe(CreateNew);
            EventAggregator.GetEvent<OpenAnimationEvent>().Subscribe(Open);
            EventAggregator.GetEvent<SaveAnimationEvent>().Subscribe(Save);
            EventAggregator.GetEvent<SaveAnimationAsEvent>().Subscribe(SaveAs);

            EventAggregator.GetEvent<DeleteFrameViewModelEvent>().Subscribe(DeleteFrame);
        }

        #endregion

        #region Properties

        private readonly IDialogService _dialogService;

        public string CurrentFilePath { get; set; }

        public FrameViewModel CurrentFrame
        {
            get { return _currentFrame; }
            set
            {
                if (Equals(value, _currentFrame)) return;
                _currentFrame = value;
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
                    FrameViewModels = new ObservableCollection<FrameViewModel>();
                    foreach (var frame in value.Frames)
                    {
                        FrameViewModels.Add(CreateFrameViewModel(frame));
                    }
                    CurrentFrame = FrameViewModels.FirstOrDefault();
                }

                OnPropertyChanged();
            }
        }

        public ObservableCollection<FrameViewModel> FrameViewModels
        {
            get { return _frameViewModels; }
            private set
            {
                _frameViewModels = value;
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
            var dialogResult = _dialogService.ShowDialog("Create new animation", Container.Resolve<NewAnimationViewModel>()) as NewAnimationViewModel;
            if (dialogResult != null)
            {
                Animation = CreateNewAnimation(dialogResult.SelectedColorDepth, dialogResult.FrameWidth,
                    dialogResult.FrameHeight);
                EventAggregator.GetEvent<StatusBarMessageChangeEvent>().Publish("New animation created.");
            }
        }

        private void Open(object obj)
        {
            Stream fileStream;
            string filePath;
            var dialogResult = _dialogService.ShowOpenFileDialog("Pixel Matrix Animation (*.pma)|*.pma|All files (*.*)|*.*", out fileStream, out filePath);
            if (dialogResult == DialogResult.Ok)
            {
                var serializer = new AnimationSerializer();             
                Animation = serializer.Deserialize(fileStream);
                CurrentFilePath = filePath;
                EventAggregator.GetEvent<StatusBarMessageChangeEvent>().Publish("Animation loaded.");
            }
        }

        private void Save(object obj)
        {
            if (!String.IsNullOrEmpty(CurrentFilePath))
            {
                SaveAnimationTo(Animation, CurrentFilePath);
                EventAggregator.GetEvent<StatusBarMessageChangeEvent>().Publish("Saving complete.");
            }
        }

        private void SaveAs(object obj)
        {
            string filePath;
            var dialogResult = _dialogService.ShowSaveFileDialog("Pixel Matrix Animation (*.pma)|*.pma|All files (*.*)|*.*","untitled.pma", out filePath);
            if (dialogResult == DialogResult.Ok)
            {
                SaveAnimationTo(Animation, filePath);
                EventAggregator.GetEvent<StatusBarMessageChangeEvent>().Publish("Saving complete.");
            }
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
            var dialogResult = _dialogService.ShowPrompt("Are you sure you want to delete the Frame?", "Warning!");
            if (dialogResult == DialogResult.Ok)
            {
                FrameViewModels.Remove((FrameViewModel)obj);
            }
        }

        private void AddFrame(object obj)
        {
            var newFrame = CreateFrameViewModel(null);
            FrameViewModels.Add(newFrame);
        }

        private FrameViewModel CreateFrameViewModel(Frame<byte> frame)
        {
            var newFrame = Container.Resolve<FrameViewModel>();
            newFrame.Frame = frame ?? new Frame<byte>(_currentAnimationFrameWidth, _currentAnimationFrameHeight, _animation.ColorDepth);
            return newFrame;
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

        #region Private State

        private Animation _animation;

        private DelegateCommand<object> _addFrameCommand;

        private short _currentAnimationFrameWidth = 0;
        private short _currentAnimationFrameHeight = 0;
        private FrameViewModel _currentFrame;
        private ObservableCollection<FrameViewModel> _frameViewModels;

        #endregion
        #endregion
    }
}