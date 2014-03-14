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
                return new List<int> { 1, 2, 4, 8 };
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
        #region File Menu
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

        #region Edit Menu

        public DelegateCommand<object> CopyCommand
        {
            get { return _copyCommand ?? (_copyCommand = new DelegateCommand<object>(Copy)); }
        }
        public DelegateCommand<object> PasteCommand
        {
            get { return _pasteCommand ?? (_pasteCommand = new DelegateCommand<object>(Paste)); }
        }
        public DelegateCommand<object> GotoCommand
        {
            get { return _gotoCommand ?? (_gotoCommand = new DelegateCommand<object>(Goto)); }
        }
        public DelegateCommand<object> BatchChangeDurationCommand
        {
            get { return _batchChangeDurationCommand ?? (_batchChangeDurationCommand = new DelegateCommand<object>(BatchChangeDuration)); }
        }

        #endregion

        #region Help Menu

        public DelegateCommand<object> AboutCommand
        {
            get { return _aboutCommand ?? (_aboutCommand = new DelegateCommand<object>(About)); }
        }

        #endregion

        #region Toolbar
        public DelegateCommand<object> ToggleGridCommand
        {
            get { return _toggleGridCommand ?? (_toggleGridCommand = new DelegateCommand<object>(ToggleGrid)); }
        }

        public DelegateCommand<object> ToggleGhostCommand
        {
            get { return _toggleGhostCommand ?? (_toggleGhostCommand = new DelegateCommand<object>(ToggleGhost)); }
        }

        #endregion

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
        private void Copy(object obj)
        {
            EventAggregator.GetEvent<CopyEvent>().Publish(0);
        }
        private void Paste(object obj)
        {
            EventAggregator.GetEvent<PasteEvent>().Publish(0);
        }
        private void Goto(object obj)
        {
            EventAggregator.GetEvent<GotoEvent>().Publish(0);
        }
        private void BatchChangeDuration(object obj)
        {
            EventAggregator.GetEvent<BatchChangeDurationEvent>().Publish(0);
        }
        private void About(object obj)
        {
            EventAggregator.GetEvent<AboutEvent>().Publish(0);
        }
        private void ToggleGrid(object obj)
        {
            EventAggregator.GetEvent<ToggleGridVisibilityEvent>().Publish(true);
        }

        private void ToggleGhost(object obj)
        {
            EventAggregator.GetEvent<ToggleGhostVisibilityEvent>().Publish(true);
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

        private DelegateCommand<object> _copyCommand;
        private DelegateCommand<object> _pasteCommand;
        private DelegateCommand<object> _gotoCommand;
        private DelegateCommand<object> _batchChangeDurationCommand;

        private DelegateCommand<object> _aboutCommand;
        private DelegateCommand<object> _toggleGridCommand;
        private DelegateCommand<object> _toggleGhostCommand;

        private byte _selectedShadeLevel = 200;
        private int _selectedBrushSize = 1;

        
        #endregion
        #endregion
    }
}
