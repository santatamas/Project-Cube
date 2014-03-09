using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using CubeProject.Data.Entities;
using CubeProject.Data.Serializers;
using CubeProject.Infrastructure.BaseClasses;
using CubeProject.Infrastructure.Enums;
using CubeProject.Infrastructure.Events;
using CubeProject.Infrastructure.Interfaces;
using CubeProject.Infrastructure.Utility;
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
            FrameViewModels = new ObservableCollection<IFrameViewModel>();

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

            EventAggregator.GetEvent<NextFrameEvent>().Subscribe(NextFrame);
            EventAggregator.GetEvent<PreviousFrameEvent>().Subscribe(PreviousFrame);
            EventAggregator.GetEvent<PlayEvent>().Subscribe(Play);
            EventAggregator.GetEvent<PauseEvent>().Subscribe(Pause);
            EventAggregator.GetEvent<StopEvent>().Subscribe(Stop);

            EventAggregator.GetEvent<CopyEvent>().Subscribe(Copy);
            EventAggregator.GetEvent<PasteEvent>().Subscribe(Paste);

            EventAggregator.GetEvent<CopyContentEvent>().Subscribe(CopyContent);
            EventAggregator.GetEvent<PasteContentEvent>().Subscribe(PasteContent);

        }

        #endregion

        #region Properties

        private readonly IDialogService _dialogService;

        public string CurrentFilePath { get; set; }

        public IFrameViewModel CurrentFrame
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
                    FrameViewModels = new ObservableCollection<IFrameViewModel>();
                    foreach (var frame in value.Frames)
                    {
                        FrameViewModels.Add(CreateFrameViewModel(frame));
                    }
                    CurrentFrame = FrameViewModels.FirstOrDefault();
                }

                OnPropertyChanged();
            }
        }

        public ObservableCollection<IFrameViewModel> FrameViewModels
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
            var dialogResult = (NewAnimationViewModel)_dialogService.ShowDialog("Create new animation", Container.Resolve<NewAnimationViewModel>());
            Animation = CreateNewAnimation(dialogResult.SelectedColorDepth, dialogResult.FrameWidth, dialogResult.FrameHeight);
            EventAggregator.GetEvent<StatusBarMessageChangeEvent>().Publish("New animation created.");
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

        private IFrameViewModel CreateFrameViewModel(IFrame<byte> frame)
        {
            var newFrame = Container.Resolve<IFrameViewModel>();
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
                animation.Frames.Add(new Frame<byte>(frameWidth, frameHeight){Duration = 500});
            }

            _currentAnimationFrameWidth = frameWidth;
            _currentAnimationFrameHeight = frameHeight;

            return animation;
        }

        private void Stop(int obj)
        {
            _isPlaying = false;
            EventAggregator.GetEvent<StoppedEvent>().Publish(0);
            CurrentFrame = FrameViewModels[0];
        }

        private void Pause(int obj)
        {
            _isPlaying = false;
        }

        private void Play(int obj)
        {
            if (_playerThread == null)
            {
                _playerThread = new Thread(new ThreadStart(DoPlay));
                _playerThread.Start();
            }
            _isPlaying = true;
        }

        private void DoPlay()
        {
            while (true)
            {
                // get the CPU a little rest here
                Thread.Sleep(10);

                if (!_isPlaying) continue;

                var currentFrameIndex = FrameViewModels.IndexOf(CurrentFrame);
                if (FrameViewModels.Count > currentFrameIndex + 1)
                {
                    Thread.Sleep(FrameViewModels[currentFrameIndex].Frame.Duration);
                    
                    // if we pressed STOP while waiting for the next frame, quit animating now
                    if (!_isPlaying) continue;

                    Dispatcher.CurrentDispatcher.Invoke(() => NextFrame(0));

                }
                else
                {
                    _isPlaying = false;
                    EventAggregator.GetEvent<StoppedEvent>().Publish(0);
                }
            }
        }

        private void PreviousFrame(int obj)
        {
            var currentFrameIndex = FrameViewModels.IndexOf(CurrentFrame);
            if(currentFrameIndex - 1 >= 0)
                CurrentFrame = FrameViewModels[currentFrameIndex - 1];
        }

        private void NextFrame(int obj)
        {
            var currentFrameIndex = FrameViewModels.IndexOf(CurrentFrame);
            if (FrameViewModels.Count > currentFrameIndex + 1)
            {
                CurrentFrame = FrameViewModels[currentFrameIndex + 1];
            }
        }

        private void Paste(int obj)
        {
            var index = FrameViewModels.IndexOf(CurrentFrame);
            FrameViewModels.Insert(index+1, CreateFrameViewModel(DeepCopy.Make(_clipBoardFVM.Frame)));
        }

        private void Copy(int obj)
        {
            _clipBoardFVM = CurrentFrame;
        }

        private void PasteContent(object frameViewModel)
        {
            if (_clipBoardFrame == null) return;

            var model = frameViewModel as IFrameViewModel;
            if (model == null) return;

            model.Frame = _clipBoardFrame;
            model.ReDraw();
        }

        private void CopyContent(object frame)
        {
            _clipBoardFrame = DeepCopy.Make(frame as Frame<byte>);
        }

        #region Private State

        private Animation _animation;

        private DelegateCommand<object> _addFrameCommand;

        private short _currentAnimationFrameWidth = 0;
        private short _currentAnimationFrameHeight = 0;
        private IFrameViewModel _currentFrame;
        private ObservableCollection<IFrameViewModel> _frameViewModels;

        private Thread _playerThread = null;
        private bool _isPlaying = false;

        private IFrameViewModel _clipBoardFVM = null;
        private Frame<byte> _clipBoardFrame = null;

        #endregion

        #endregion
    }
}