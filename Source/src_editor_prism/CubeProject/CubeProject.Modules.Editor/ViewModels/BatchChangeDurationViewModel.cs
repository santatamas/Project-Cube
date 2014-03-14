using CubeProject.Infrastructure.BaseClasses;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

namespace CubeProject.Modules.Editor.ViewModels
{
    public class BatchChangeDurationViewModel : DialogViewModelBase
    {
        private int _startIndex;
        private int _endIndex;
        private short _duration;

        public int StartIndex
        {
            get { return _startIndex; }
            set
            {
                _startIndex = value;
                OnPropertyChanged();
            }
        }

        public int EndIndex
        {
            get { return _endIndex; }
            set
            {
                _endIndex = value;
                OnPropertyChanged();
            }
        }

        public short Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                OnPropertyChanged();
            }
        }

        public BatchChangeDurationViewModel(IUnityContainer container, IEventAggregator aggregator) : base(container, aggregator)
        {
        }
    }
}
