using System;
using System.Windows;
using CubeProject.Infrastructure.Interfaces;

namespace CubeProject.Modules.Common.Services
{
    /// <summary>
    /// Interaction logic for WindowDialog.xaml
    /// </summary>
    public partial class WindowDialog : Window
    {
        public WindowDialog()
        {
            InitializeComponent();
            this.DataContextChanged += WindowDialog_DataContextChanged;
        }

        void WindowDialog_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var oldContext = e.OldValue as IDialogViewModel;
            var newContext = e.NewValue as IDialogViewModel;

            if (oldContext != null)
            {
                oldContext.OkExecuted -= ContextOkExecuted;
            }

            if (newContext != null)
            {
                newContext.OkExecuted += ContextOkExecuted;
            }
        }

        private void ContextOkExecuted(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
