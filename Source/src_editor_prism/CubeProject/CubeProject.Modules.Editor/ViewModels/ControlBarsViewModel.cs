using System.Collections.Generic;
using CubeProject.Infrastructure.BaseClasses;
using CubeProject.Infrastructure.Events;
using CubeProject.Infrastructure.Interfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

namespace CubeProject.Modules.Editor.ViewModels
{
    public class ControlBarsViewModel : ViewModelBase, IControlBarsViewModel
    {
        #region Properties
        public int SelectedBrushSize
        {
            get { return _selectedBrushSize; }
            set
            {
                if (value == _selectedBrushSize) return;
                _selectedBrushSize = value;
                EventAggregator.GetEvent<BrushSizeChangedEvent>().Publish(_selectedBrushSize);
                OnPropertyChanged();
            }
        }
        public List<int> BrushSize
        {
            get
            {
                return new List<int> { 1, 2, 3, 4 };
            }
        }
        public byte SelectedShadeLevel
        {
            get { return _selectedShadeLevel; }
            set
            {
                _selectedShadeLevel = value;
                EventAggregator.GetEvent<ShadeChangedEvent>().Publish(_selectedShadeLevel);
                OnPropertyChanged();
            }
        }
        public byte[] AvaliableShadeLevels
        {
            get
            {
                return new byte[] { 50, 100, 150, 200, 255 };
            }
        }
        #endregion

        public ControlBarsViewModel(IUnityContainer container, IEventAggregator aggregator) : base(container, aggregator)
        {
            aggregator.GetEvent<RequestBrushSizeEvent>().Subscribe(HandleRequestBrushSize);
            aggregator.GetEvent<RequestShadeEvent>().Subscribe(HandleRequestShade);
        }

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
        private void CreateNew(object obj)
        {
            EventAggregator.GetEvent<CreateNewAnimationEvent>().Publish("");
        }
        private void Open(object obj)
        {
            EventAggregator.GetEvent<OpenAnimationEvent>().Publish("");
        }
        private void Save(object obj)
        {
            EventAggregator.GetEvent<SaveAnimationEvent>().Publish("");
        }
        private void SaveAs(object obj)
        {
            EventAggregator.GetEvent<SaveAnimationAsEvent>().Publish("");
        }
        private void CloseApplication(object obj)
        {
            EventAggregator.GetEvent<CloseApplicationEvent>().Publish("");
        }
        private void HandleRequestShade(byte obj)
        {
            EventAggregator.GetEvent<ShadeChangedEvent>().Publish(_selectedShadeLevel);
        }

        private void HandleRequestBrushSize(int obj)
        {
            EventAggregator.GetEvent<BrushSizeChangedEvent>().Publish(_selectedBrushSize);
        }

        #region Private State
        private DelegateCommand<object> _newCommand;
        private DelegateCommand<object> _openCommand;
        private DelegateCommand<object> _saveCommand;
        private DelegateCommand<object> _saveAsCommand;
        private DelegateCommand<object> _closeCommand;

        private byte _selectedShadeLevel = 200;
        private int _selectedBrushSize = 1;
        #endregion
        #endregion
    }
}
