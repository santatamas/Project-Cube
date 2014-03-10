using System;
using CubeProject.Infrastructure.Interfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

namespace CubeProject.Infrastructure.BaseClasses
{
    /// <summary>
    /// Provides basic functionality for ViewModels that are intended to be used as dialogs.
    /// </summary>
    public class DialogViewModelBase : ViewModelBase, IDialogResultProvider, IDialogViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DialogViewModelBase"/> class.
        /// </summary>
        /// <param name="container">The unity container.</param>
        /// <param name="aggregator">The event aggregator.</param>
        public DialogViewModelBase(IUnityContainer container, IEventAggregator aggregator) : base(container, aggregator)
        {
        }

        /// <summary>
        /// Gets the ok command.
        /// </summary>
        /// <value>
        /// The ok command.
        /// </value>
        public DelegateCommand<object> OkCommand
        {
            get { return _okCommand ?? (_okCommand = new DelegateCommand<object>(Ok)); }
        }

        /// <summary>
        /// Gets the dialog result.
        /// </summary>
        /// <value>
        /// The dialog result.
        /// </value>
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

        /// <summary>
        /// Occurs when the Ok command has been executed.
        /// </summary>
        public event System.EventHandler OkExecuted;
        private DelegateCommand<object> _okCommand;

        private void Ok(object obj)
        {
            if (OkExecuted != null)
                OkExecuted(this, new EventArgs());
        }
    }
}
