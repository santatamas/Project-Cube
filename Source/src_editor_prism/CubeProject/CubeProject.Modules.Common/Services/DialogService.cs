using System;
using System.IO;
using System.Windows;
using CubeProject.Infrastructure.Enums;
using CubeProject.Infrastructure.Interfaces;
using Microsoft.Win32;

namespace CubeProject.Modules.Common.Services
{
    /// <summary>
    /// Provides WPF implementation for <see cref="IDialogService"/>.
    /// </summary>
    public class DialogService : IDialogService
    {
        public void ShowMessage(string message, string caption)
        {
            MessageBox.Show(message, caption);
        }

        public DialogResult ShowPrompt(string message, string caption)
        {
            MessageBoxResult result = MessageBox.Show(message, caption, MessageBoxButton.YesNoCancel);

            switch (result)
            {
                case MessageBoxResult.None:
                    return DialogResult.Cancel;
                case MessageBoxResult.OK:
                    return DialogResult.Ok;
                case MessageBoxResult.Cancel:
                    return DialogResult.Cancel;
                case MessageBoxResult.Yes:
                    return DialogResult.Ok;
                case MessageBoxResult.No:
                    return DialogResult.No;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public DialogResult ShowOpenFileDialog(string filterText, out Stream fileStream, out string filePath)
        {
            fileStream = null;
            filePath = null;

            var ofd = new OpenFileDialog { Filter = filterText };
            if (ofd.ShowDialog() != true)
                return DialogResult.Cancel;

            fileStream = ofd.OpenFile();
            filePath = ofd.FileName;

            return DialogResult.Ok;
        }

        public DialogResult ShowSaveFileDialog(string filterText, string defaultFileName, out string filePath)
        {
            filePath = string.Empty;
            var sfd = new SaveFileDialog { Filter = filterText, FileName = defaultFileName};
            if (sfd.ShowDialog() != true)
                return DialogResult.Cancel;

            filePath = sfd.FileName;
            return DialogResult.Ok;
        }

        public object ShowDialog(string title, IDialogResultProvider dataContext)
        {
            var win = new WindowDialog {Title = title, DataContext = dataContext};
            win.ShowDialog();

            return dataContext.DialogResult;
        }
    }
}
