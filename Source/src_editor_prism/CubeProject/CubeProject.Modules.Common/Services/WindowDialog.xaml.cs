using System;
using System.Windows;
using CubeProject.Infrastructure.Interfaces;
using CubeProject.UIExtensions;

namespace CubeProject.Modules.Common.Services
{
    /// <summary>
    /// Interaction logic for WindowDialog.xaml
    /// </summary>
    public partial class WindowDialog : CustomChromeWindow
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
                oldContext.OkTriggered -= context_OkTriggered;
            }

            if (newContext != null)
            {
                newContext.OkTriggered += context_OkTriggered;
            }
        }

        private void context_OkTriggered(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
