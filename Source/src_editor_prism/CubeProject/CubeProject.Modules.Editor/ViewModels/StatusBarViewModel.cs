using CubeProject.Infrastructure.BaseClasses;
using CubeProject.Infrastructure.Events;
using CubeProject.Infrastructure.Interfaces;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

namespace CubeProject.Modules.Editor.ViewModels
{
    public class StatusBarViewModel : ViewModelBase, IStatusBarViewModel
    {
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
        private string _activeStatusMessage;

        public StatusBarViewModel(IUnityContainer container, IEventAggregator aggregator) : base(container, aggregator)
        {
            aggregator.GetEvent<StatusBarMessageChangeEvent>().Subscribe(ChangeMessage);
        }

        private void ChangeMessage(string message)
        {
            ActiveStatusMessage = message;
        }
    }
}
