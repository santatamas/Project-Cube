﻿using System.IO;
using CubeProject.Infrastructure.Enums;

namespace CubeProject.Infrastructure.Interfaces
{
    public interface IDialogService
    {
        void ShowMessage(string message, string caption);

        DialogResult ShowPrompt(string message, string caption);

        DialogResult ShowOpenFileDialog(string filterText, out Stream fileStream, out string filePath);

        DialogResult ShowSaveFileDialog(string filterText, string defaultFileName, out string filePath);

        object ShowDialog(string title, IDialogResultProvider dataContext);
    }
}
