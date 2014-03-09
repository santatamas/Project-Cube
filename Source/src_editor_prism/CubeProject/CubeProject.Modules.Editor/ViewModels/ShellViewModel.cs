using CubeProject.Infrastructure.BaseClasses;
using CubeProject.Infrastructure.Events;
using CubeProject.Infrastructure.Interfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

namespace CubeProject.Modules.Editor.ViewModels
{
    public class ShellViewModel : ViewModelBase, IShellViewModel
    {
        private DelegateCommand<object> _copyCommand;
        private DelegateCommand<object> _pasteCommand;

        public ShellViewModel(IUnityContainer container, IEventAggregator aggregator) : base(container, aggregator)
        {
        }

        public DelegateCommand<object> CopyCommand
        {
            get { return _copyCommand ?? (_copyCommand = new DelegateCommand<object>(CopyCommandHandler)); }
        }
        public DelegateCommand<object> PasteCommand
        {
            get { return _pasteCommand ?? (_pasteCommand = new DelegateCommand<object>(PasteCommandHandler)); }
        }

        private void CopyCommandHandler(object obj)
        {
            EventAggregator.GetEvent<CopyEvent>().Publish(0);
        }
        private void PasteCommandHandler(object obj)
        {
            EventAggregator.GetEvent<PasteEvent>().Publish(0);
        }
    }
}
