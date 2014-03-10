using System.IO;
using CubeProject.Infrastructure.Enums;

namespace CubeProject.Infrastructure.Interfaces
{
    /// <summary>
    /// Capable to instantiate dialogs.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Shows the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The window caption.</param>
        void ShowMessage(string message, string caption);

        /// <summary>
        /// Shows a basic prompt dialog.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The caption.</param>
        /// <returns></returns>
        DialogResult ShowPrompt(string message, string caption);

        /// <summary>
        /// Shows an open file dialog.
        /// </summary>
        /// <param name="filterText">The filter text.</param>
        /// <param name="fileStream">The file stream.</param>
        /// <param name="filePath">The file path.</param>
        /// <returns>The result of the dialog.</returns>
        DialogResult ShowOpenFileDialog(string filterText, out Stream fileStream, out string filePath);

        /// <summary>
        /// Shows a save file dialog.
        /// </summary>
        /// <param name="filterText">The filter text.</param>
        /// <param name="defaultFileName">Default name of the file.</param>
        /// <param name="filePath">The file path.</param>
        /// <returns>The result of the dialog.</returns>
        DialogResult ShowSaveFileDialog(string filterText, string defaultFileName, out string filePath);

        /// <summary>
        /// Shows a dialog with the custom viewmodel.
        /// The ViewModel should provide a dialogresult, which is returned as soon as the dialog has been closed.
        /// HINT: The view for the provided viewmodel is resolved via a datatemplate in App.xaml
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="dataContext">The data context.</param>
        /// <returns></returns>
        object ShowDialog(string title, IDialogResultProvider dataContext);
    }
}
