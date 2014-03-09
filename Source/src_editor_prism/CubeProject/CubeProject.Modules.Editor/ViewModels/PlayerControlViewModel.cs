using CubeProject.Infrastructure.BaseClasses;
using CubeProject.Infrastructure.Events;
using CubeProject.Infrastructure.Interfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

namespace CubeProject.Modules.Editor.ViewModels
{
    public class PlayerControlViewModel : ViewModelBase, IPlayerControlViewModel
    {
        private DelegateCommand<object> _previousCommand;
        private DelegateCommand<object> _pauseCommand;
        private DelegateCommand<object> _playCommand;
        private DelegateCommand<object> _nextCommand;
        private DelegateCommand<object> _stopCommand;
        private bool _isPlayVisible = true;
        private bool _isPauseVisible = false;

        public bool IsPlayVisible
        {
            get { return _isPlayVisible; }
            set
            {
                _isPlayVisible = value;
                OnPropertyChanged();
            }
        }
        public bool IsPauseVisible
        {
            get { return _isPauseVisible; }
            set
            {
                _isPauseVisible = value;
                OnPropertyChanged();
            }
        }

        public PlayerControlViewModel(IUnityContainer container, IEventAggregator aggregator)
            : base(container, aggregator)
        {
            aggregator.GetEvent<StoppedEvent>().Subscribe(ResetControlStates);
        }

        private void ResetControlStates(int obj)
        {
            IsPlayVisible = true;
            IsPauseVisible = false;
        }

        #region Commands
        public DelegateCommand<object> PreviousCommand
        {
            get { return _previousCommand ?? (_previousCommand = new DelegateCommand<object>(PreviousCommandHandler)); }
        }

        public DelegateCommand<object> PauseCommand
        {
            get { return _pauseCommand ?? (_pauseCommand = new DelegateCommand<object>(PauseCommandHandler)); }
        }

        public DelegateCommand<object> PlayCommand
        {
            get { return _playCommand ?? (_playCommand = new DelegateCommand<object>(PlayCommandHandler)); }
        }

        public DelegateCommand<object> NextCommand
        {
            get { return _nextCommand ?? (_nextCommand = new DelegateCommand<object>(NextCommandHandler)); }
        }

        public DelegateCommand<object> StopCommand
        {
            get { return _stopCommand ?? (_stopCommand = new DelegateCommand<object>(StopCommandHandler)); }
        }

        #endregion

        private void PreviousCommandHandler(object obj)
        {
            EventAggregator.GetEvent<PreviousFrameEvent>().Publish(0);
        }

        private void PauseCommandHandler(object obj)
        {
            EventAggregator.GetEvent<PauseEvent>().Publish(0);
            IsPlayVisible = true;
            IsPauseVisible = false;
        }

        private void PlayCommandHandler(object obj)
        {
            EventAggregator.GetEvent<PlayEvent>().Publish(0);
            IsPlayVisible = false;
            IsPauseVisible = true;
        }

        private void NextCommandHandler(object obj)
        {
            EventAggregator.GetEvent<NextFrameEvent>().Publish(0);
        }

        private void StopCommandHandler(object obj)
        {
            EventAggregator.GetEvent<StopEvent>().Publish(0);
        }
    }
}
