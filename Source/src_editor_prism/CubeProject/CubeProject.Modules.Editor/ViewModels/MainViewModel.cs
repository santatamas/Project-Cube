using System;
using System.Collections.ObjectModel;
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
            _dialogService = container.Resolve<IDialogService>();

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            EventAggregator.GetEvent<CloseApplicationEvent>().Subscribe(CloseApplication);
            EventAggregator.GetEvent<CreateNewAnimationEvent>().Subscribe(CreateNew);
            EventAggregator.GetEvent<OpenAnimationEvent>().Subscribe(Open);
            EventAggregator.GetEvent<SaveAnimationEvent>().Subscribe(Save);
            EventAggregator.GetEvent<SaveAnimationAsEvent>().Subscribe(SaveAs);

            EventAggregator.GetEvent<DeleteFrameViewModelEvent>().Subscribe(DeleteFrame);

            EventAggregator.GetEvent<BrushSizeChangedEvent>().Subscribe(BrushSizeChanged);
            EventAggregator.GetEvent<ShadeChangedEvent>().Subscribe(ShadeChanged);
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

                OnPropertyChanged();
            }
        }

        public ObservableCollection<FrameViewModel> FrameViewModels { get; set; }

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
                EventAggregator.GetEvent<StatusBarMessageChangeEvent>().Publish("Saving complete.");
            }
        }

        private void SaveAs(object obj)
        {
            string filePath;
            var dialogResult = _dialogService.ShowSaveFileDialog("Pixel Matrix Animation (*.pma)|*.pma|All files (*.*)|*.*", out filePath);
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

        private void ShadeChanged(int obj)
        {

        }

        private void BrushSizeChanged(int obj)
        {

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

        #endregion
        #endregion
    }
}