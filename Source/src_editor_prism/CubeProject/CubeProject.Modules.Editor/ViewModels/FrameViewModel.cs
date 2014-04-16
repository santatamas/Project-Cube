using System;
using CubeProject.Graphics;
using CubeProject.Infrastructure.BaseClasses;
using CubeProject.Infrastructure.Events;
using CubeProject.Infrastructure.Interfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using CubeProject.Infrastructure.Enums;

namespace CubeProject.Modules.Editor.ViewModels
{
    public class FrameViewModel : ViewModelBase, IFrameViewModel
    {
        public FrameViewModel(IUnityContainer container, IEventAggregator aggregator, IDialogService dialogService)
            : base(container, aggregator)
        {
            _dialogService = dialogService;

            EventAggregator.GetEvent<BrushSizeChangedEvent>().Subscribe(BrushSizeChanged);
            EventAggregator.GetEvent<ShadeChangedEvent>().Subscribe(ShadeChanged);
            EventAggregator.GetEvent<ToggleGridVisibilityEvent>().Subscribe(ToggleGridVisibility);

            EventAggregator.GetEvent<RequestBrushSizeEvent>().Publish(0);
            EventAggregator.GetEvent<RequestShadeEvent>().Publish(0);
        }

        #region Properties

        public RendererSettings Settings
        {
            get { return _settings; }
        }

        public bool IsGridVisible
        {
            get { return _isGridVisible; }
            set
            {
                _isGridVisible = value;
                OnPropertyChanged();
            }
        }

        public IFrame<byte> Frame
        {
            get { return _frame; }
            set
            {
                _frame = value;
                if (value != null)
                {
                    _settings = new RendererSettings()
                    {
                        ColorDepth = _frame.ColorDepth,
                        GapSize = 2,
                        PixelSize = 8,
                        ScreenHeight = _frame.Height*(2 + 8),
                        ScreenWidth = _frame.Width*(2 + 8),
                        SizeX = _frame.Width,
                        SizeY = _frame.Height
                    };
                }
                OnPropertyChanged();
            }
        }

        public event EventHandler FrameChanged;

        protected virtual void OnFrameChanged()
        {
            EventHandler handler = FrameChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public int Index
        {
            get { return _index; }
            set
            {
                _index = value;
                OnPropertyChanged();
            }
        }

        public void InvokeOnFrameChanged()
        {
            OnFrameChanged();
        }


        public Int16 Duration {
            get
            {
                return Frame.Duration;
            }
            set
            {
                Frame.Duration = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public DelegateCommand<object> DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new DelegateCommand<object>(Delete)); }
        }
        public DelegateCommand<object> ChangeDurationCommand
        {
            get { return _changeDurationCommand ?? (_changeDurationCommand = new DelegateCommand<object>(ChangeDurationCommandHandler)); }
        }
        public DelegateCommand<object> CopyCommand
        {
            get { return _copyCommand ?? (_copyCommand = new DelegateCommand<object>(CopyCommandHandler)); }
        }

        public DelegateCommand<object> PasteCommand
        {
            get { return _pasteCommand ?? (_pasteCommand = new DelegateCommand<object>(PasteCommandHandler)); }
        }

        public DelegateCommand<object> ExportImageCommand
        {
            get { return _exportImageCommand ?? (_exportImageCommand = new DelegateCommand<object>(ExportImageCommandHandler)); }
        }


        public int BrushSize
        {
            get { return _brushSize; }
        }

        public byte BrushShade
        {
            get { return _brushShade; }
        }

        #endregion

        #region Private
        private void ExportImageCommandHandler(object obj)
        {
            string filePath;
            var dialogResult = _dialogService.ShowSaveFileDialog("Image (*.bmp)|*.bmp", "untitled.bmp", out filePath);
            if (dialogResult == DialogResult.Ok)
            {
                
                EventAggregator.GetEvent<StatusBarMessageChangeEvent>().Publish("Export complete.");
            }
        }

        private void CopyCommandHandler(object obj)
        {
            EventAggregator.GetEvent<CopyContentEvent>().Publish(Frame);
        }

        private void PasteCommandHandler(object obj)
        {
            EventAggregator.GetEvent<PasteContentEvent>().Publish(this);
        }

        private void ChangeDurationCommandHandler(object obj)
        {
            var dialogViewModel = Container.Resolve<IChangeDurationViewModel>();
            dialogViewModel.Duration = Frame.Duration;
            _dialogService.ShowDialog("Change Duration", (IDialogResultProvider)dialogViewModel);

            Frame.Duration = dialogViewModel.Duration;
            OnPropertyChanged("Duration");
        }

        private void Delete(object obj)
        {
            EventAggregator.GetEvent<DeleteFrameViewModelEvent>().Publish(this);
        }

        private void ShadeChanged(byte shade)
        {
            _brushShade = (byte)shade;
        }

        private void BrushSizeChanged(int size)
        {
            _brushSize = size;
        }

        private void ToggleGridVisibility(bool obj)
        {
            IsGridVisible = obj;
        }

        #region Private State

        private int _brushSize = 0;
        private byte _brushShade = 255;


        private RendererSettings _settings;
        private IFrame<byte> _frame;
        private DelegateCommand<object> _deleteCommand;
        private DelegateCommand<object> _changeDurationCommand;
        private DelegateCommand<object> _copyCommand;
        private DelegateCommand<object> _pasteCommand;
        private DelegateCommand<object> _exportImageCommand;

        private IDialogService _dialogService;
        private bool _isGridVisible = true;
        private int _index;

        #endregion
        #endregion    
    }
}
