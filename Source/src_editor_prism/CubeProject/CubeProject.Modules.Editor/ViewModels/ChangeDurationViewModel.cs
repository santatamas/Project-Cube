using System;
using CubeProject.Infrastructure.BaseClasses;
using CubeProject.Infrastructure.Interfaces;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

namespace CubeProject.Modules.Editor.ViewModels
{
    public class ChangeDurationViewModel : DialogViewModelBase, IChangeDurationViewModel
    {
        private short _duration;

        public Int16 Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                OnPropertyChanged();
            }
        }

        public ChangeDurationViewModel(IUnityContainer container, IEventAggregator aggregator) : base(container, aggregator)
        {
        }
    }
}
