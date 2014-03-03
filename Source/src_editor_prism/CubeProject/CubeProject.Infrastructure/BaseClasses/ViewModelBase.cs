using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

namespace CubeProject.Infrastructure.BaseClasses
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public IUnityContainer Container { get; private set; }
        public IEventAggregator EventAggregator { get; set; }

        public ViewModelBase(IUnityContainer container, IEventAggregator aggregator)
        {
            Container = container;
            EventAggregator = aggregator;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
