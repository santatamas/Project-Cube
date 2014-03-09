using System;
using CubeProject.Infrastructure.Interfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

namespace CubeProject.Infrastructure.BaseClasses
{
    public class DialogViewModelBase : ViewModelBase, IDialogResultProvider, IDialogViewModel
    {
        public DialogViewModelBase(IUnityContainer container, IEventAggregator aggregator) : base(container, aggregator)
        {
        }

        public DelegateCommand<object> OkCommand
        {
            get { return _okCommand ?? (_okCommand = new DelegateCommand<object>(Ok)); }
        }

        public object DialogResult
        {
            get
            {
                return GetDialogResult();
            }
        }

        protected virtual object GetDialogResult()
        {
            return this;
        }

        public event System.EventHandler OkTriggered;
        private DelegateCommand<object> _okCommand;

        private void Ok(object obj)
        {
            if (OkTriggered != null)
                OkTriggered(this, new EventArgs());
        }
    }
}
