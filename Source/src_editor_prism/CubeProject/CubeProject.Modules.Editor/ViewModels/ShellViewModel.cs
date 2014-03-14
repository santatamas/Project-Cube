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
        private DelegateCommand<object> _saveCommand;
        private DelegateCommand<object> _gotoCommand;
        private DelegateCommand<object> _newCommand;
        private DelegateCommand<object> _openCommand;

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
        public DelegateCommand<object> SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new DelegateCommand<object>(SaveCommandHandler)); }
        }
        public DelegateCommand<object> GotoCommand
        {
            get { return _gotoCommand ?? (_gotoCommand = new DelegateCommand<object>(GotoCommandHandler)); }
        }
        public DelegateCommand<object> NewCommand
        {
            get { return _newCommand ?? (_newCommand = new DelegateCommand<object>(NewCommandHandler)); }
        }
        public DelegateCommand<object> OpenCommand
        {
            get { return _openCommand ?? (_openCommand = new DelegateCommand<object>(OpenCommandHandler)); }
        }

        private void OpenCommandHandler(object obj)
        {
            EventAggregator.GetEvent<OpenAnimationEvent>().Publish("");
        }
        private void NewCommandHandler(object obj)
        {
            EventAggregator.GetEvent<CreateNewAnimationEvent>().Publish("");
        }
        private void GotoCommandHandler(object obj)
        {
            EventAggregator.GetEvent<GotoEvent>().Publish(0);
        }
        private void SaveCommandHandler(object obj)
        {
            EventAggregator.GetEvent<SaveAnimationEvent>().Publish("");
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
