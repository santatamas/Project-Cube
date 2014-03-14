using CubeProject.Infrastructure.BaseClasses;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

namespace CubeProject.Modules.Editor.ViewModels
{
    public class GotoFrameViewModel : DialogViewModelBase
    {
        private int? _frameNumber;

        public int? FrameNumber
        {
            get { return _frameNumber; }
            set
            {
                _frameNumber = value;
                OnPropertyChanged();
            }
        }

        public GotoFrameViewModel(IUnityContainer container, IEventAggregator aggregator) : base(container, aggregator)
        {
        }
    }
}
