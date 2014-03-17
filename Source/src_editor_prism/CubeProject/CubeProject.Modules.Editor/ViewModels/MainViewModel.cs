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
            FrameViewModels = new ObservableCollection<FrameViewModel>();

            SubscribeEvents();

            Animation = CreateNewAnimation(ColorDepth.GrayScale, 72, 72);
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
            EventAggregator.GetEvent<PreviousFrameEvent>().Subscribe(PreviousFrameHandler);
            EventAggregator.GetEvent<PlayEvent>().Subscribe(Play);
            EventAggregator.GetEvent<PauseEvent>().Subscribe(Pause);
            EventAggregator.GetEvent<StopEvent>().Subscribe(Stop);

            EventAggregator.GetEvent<CopyEvent>().Subscribe(Copy);
            EventAggregator.GetEvent<PasteEvent>().Subscribe(Paste);

            EventAggregator.GetEvent<CopyContentEvent>().Subscribe(CopyContent);
            EventAggregator.GetEvent<PasteContentEvent>().Subscribe(PasteContent);

            EventAggregator.GetEvent<GotoEvent>().Subscribe(GotoFrame);
            EventAggregator.GetEvent<BatchChangeDurationEvent>().Subscribe(BatchChangeDuration);
            EventAggregator.GetEvent<AboutEvent>().Subscribe(ShowAbout);
            EventAggregator.GetEvent<ToggleGhostVisibilityEvent>().Subscribe(ToggleGhostVisibility);

        }

        #endregion

        #region Properties

        public string CurrentFilePath { get; set; }

        public FrameViewModel CurrentFrame
        {
            get { return _currentFrame; }
            set
            {
                if (Equals(value, _currentFrame)) return;
                _currentFrame = value;
                OnPropertyChanged("PreviousFrame");
                OnPropertyChanged();
            }
        }

        public FrameViewModel PreviousFrame
        {
            get
            {
                var frameIndex = FrameViewModels.IndexOf(CurrentFrame) - 1;
                return frameIndex >= 0 ? FrameViewModels[frameIndex] : null;
            }
        }

        public bool IsGhostVisible
        {
            get { return _isGhostVisible; }
            set
            {
                _isGhostVisible = value;
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
                if (_frameViewModels != null)
                    _frameViewModels.CollectionChanged -= _frameViewModels_CollectionChanged;


                //if (_frameViewModels != null)
                //{
                //    foreach (var frameViewModel in _frameViewModels)
                //    {
                //        frameViewModel.MatrixRenderer.Dispose();
                //    }
                //    // I might be going to hell from doing this...
                //    GC.Collect();
                //}

                _frameViewModels = value;
                _frameViewModels.CollectionChanged += _frameViewModels_CollectionChanged;
                OnPropertyChanged();
            }
        }

        void _frameViewModels_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            for (int i = 0; i < _frameViewModels.Count; i++)
            {
                _frameViewModels[i].Index = i + 1;
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
            if (!dialogResult.OkPressed) return;
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
            else
            {
                SaveAs(obj);
            }
        }

        private void SaveAs(object obj)
        {
            string filePath;
            var dialogResult = _dialogService.ShowSaveFileDialog("Pixel Matrix Animation (*.pma)|*.pma|All files (*.*)|*.*","untitled.pma", out filePath);
            if (dialogResult == DialogResult.Ok)
            {
                SaveAnimationTo(Animation, filePath);
                CurrentFilePath = filePath;
                EventAggregator.GetEvent<StatusBarMessageChangeEvent>().Publish("Saving complete.");
            }
        }

        private void SaveAnimationTo(Animation animation, string path)
        {
            var serializer = new AnimationSerializer();
            animation.Frames = FrameViewModels.Select(fvm => (Frame<byte>)fvm.Frame).ToList();
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

        private FrameViewModel CreateFrameViewModel(IFrame<byte> frame)
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

                    Application.Current.Dispatcher.Invoke(() => NextFrame(0));

                }
                else
                {
                    _isPlaying = false;
                    EventAggregator.GetEvent<StoppedEvent>().Publish(0);
                }
            }
        }

        private void PreviousFrameHandler(int obj)
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

            var model = frameViewModel as FrameViewModel;
            if (model == null) return;

            model.Frame = _clipBoardFrame;
        }

        private void CopyContent(object frame)
        {
            _clipBoardFrame = DeepCopy.Make(frame as Frame<byte>);
        }

        private void GotoFrame(int obj)
        {
            var dialogResult = (GotoFrameViewModel)_dialogService.ShowDialog("Go to frame", Container.Resolve<GotoFrameViewModel>());
            var newFrameNumber = dialogResult.FrameNumber ?? 0;
            if (newFrameNumber - 1 >= FrameViewModels.Count || newFrameNumber - 1 < 0)
            {
                EventAggregator.GetEvent<StatusBarMessageChangeEvent>().Publish("[ERROR] Invalid frame number.");
                return;
            }

            CurrentFrame = FrameViewModels[--newFrameNumber];
            EventAggregator.GetEvent<StatusBarMessageChangeEvent>().Publish("[DEBUG] Frame jump successful.");
        }

        private void BatchChangeDuration(int obj)
        {
            var dialogViewModel = Container.Resolve<BatchChangeDurationViewModel>();
            dialogViewModel.StartIndex = 1;
            dialogViewModel.EndIndex = FrameViewModels.Count;

            var dialogResult = (BatchChangeDurationViewModel)_dialogService.ShowDialog("Batch change duration", dialogViewModel);

            if (dialogResult.StartIndex - 1 >= FrameViewModels.Count || dialogResult.StartIndex - 1 < 0 ||
                dialogResult.EndIndex - 1 >= FrameViewModels.Count || dialogResult.EndIndex - 1 < 0 ||
                dialogResult.StartIndex > dialogResult.EndIndex)
            {
                EventAggregator.GetEvent<StatusBarMessageChangeEvent>().Publish("[ERROR] Invalid interval.");
                return;
            }

            if (dialogResult.Duration < 0)
            {
                EventAggregator.GetEvent<StatusBarMessageChangeEvent>().Publish("[ERROR] Invalid duration.");
                return;
            }

            for (int i = dialogResult.StartIndex - 1; i <= dialogResult.EndIndex - 1; i++)
            {
                FrameViewModels[i].Duration = dialogResult.Duration;
            }

            EventAggregator.GetEvent<StatusBarMessageChangeEvent>().Publish("[DEBUG] Batch duration change succesful.");
        }

        private void ShowAbout(int obj)
        {
            _dialogService.ShowMessage("PixelMatrixEditor - 2014 \nTamas Santa (thomas.felis@gmail.com) \n\nSpecial thanks to: \nDante Hardy", "About");
        }

        private void ToggleGhostVisibility(bool obj)
        {
            IsGhostVisible = !IsGhostVisible;
        }

        #region Private State

        private Animation _animation;

        private DelegateCommand<object> _addFrameCommand;

        private short _currentAnimationFrameWidth = 0;
        private short _currentAnimationFrameHeight = 0;
        private FrameViewModel _currentFrame;
        private ObservableCollection<FrameViewModel> _frameViewModels;

        private Thread _playerThread = null;
        private bool _isPlaying = false;

        private FrameViewModel _clipBoardFVM = null;
        private Frame<byte> _clipBoardFrame = null;
        private bool _isGhostVisible = false;
        private readonly IDialogService _dialogService;

        #endregion

        #endregion
    }
}