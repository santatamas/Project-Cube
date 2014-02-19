using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using CubeProject.Data.Entities;
using Microsoft.Practices.Prism.Commands;
using PixelMatrixEditor.Annotations;
using PixelMatrixEditor.Data;
using PixelMatrixEditor.Models;

namespace PixelMatrixEditor.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Construction
        public MainViewModel()
        {
            SelectedAreaSize = 1;
        }
        #endregion

        #region Properties
        public int SelectedAreaSize
        {
            get { return _selectedAreaSize; }
            set
            {
                if (value == _selectedAreaSize) return;
                _selectedAreaSize = value;
                OnPropertyChanged();
            }
        }

        public List<int> AreaSize
        {
            get
            {
                return new List<int> { 1, 2, 3, 4 };
            }
        }

        public Frame<byte> CurrentMatrix
        {
            get
            {
                return _currentMatrix ?? (_currentMatrix = new Frame<byte>(72, 72));
            }
        }

        #endregion

        #region Commands

        public DelegateCommand<object> NewCommand
        {
            get { return _newCommand ?? (_newCommand = new DelegateCommand<object>(CreateNew)); }
        }
        public DelegateCommand<object> OpenCommand
        {
            get { return _openCommand ?? (_openCommand = new DelegateCommand<object>(Open)); }
        }
        public DelegateCommand<object> SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new DelegateCommand<object>(Save)); }
        }
        public DelegateCommand<object> SaveAsCommand
        {
            get { return _saveAsCommand ?? (_saveAsCommand = new DelegateCommand<object>(SaveAs)); }
        }
        public DelegateCommand<object> CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new DelegateCommand<object>(CloseApplication)); }
        }

        #endregion

        #region Private
        private void CloseApplication(object obj)
        {
            Application.Current.Shutdown();
        }

        private void CreateNew(object obj)
        {
            MessageBox.Show("Create");
        }

        private void Open(object obj)
        {
            MessageBox.Show("Open");
        }

        private void Save(object obj)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 72; i++)
            {
                for (int j = 0; j < 72; j++)
                {
                    sb.Append(_currentMatrix[j, i] + " ");
                }
                sb.Append(Environment.NewLine);
            }
            MessageBox.Show(sb.ToString());
        }

        private void SaveAs(object obj)
        {
            MessageBox.Show("Save As");
        }

        #region Private State
        private int _selectedAreaSize;
        private DelegateCommand<object> _newCommand;
        private DelegateCommand<object> _openCommand;
        private DelegateCommand<object> _saveCommand;
        private DelegateCommand<object> _saveAsCommand;
        private DelegateCommand<object> _closeCommand;
        private Frame<byte> _currentMatrix;

        #endregion
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
