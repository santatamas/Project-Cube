using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

namespace CubeProject.Infrastructure.BaseClasses
{
    /// <summary>
    /// Provides basic functionality for ViewModels, like implemented <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public IUnityContainer Container { get; private set; }
        /// <summary>
        /// Gets or sets the event aggregator.
        /// </summary>
        /// <value>
        /// The event aggregator.
        /// </value>
        public IEventAggregator EventAggregator { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        /// <param name="container">The unity container.</param>
        /// <param name="aggregator">The aevent ggregator.</param>
        public ViewModelBase(IUnityContainer container, IEventAggregator aggregator)
        {
            Container = container;
            EventAggregator = aggregator;
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
