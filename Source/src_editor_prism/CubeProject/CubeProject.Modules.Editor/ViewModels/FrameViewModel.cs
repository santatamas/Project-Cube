using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using CubeProject.Data.Entities;
using CubeProject.Graphics;
using CubeProject.Graphics.Renderers;
using CubeProject.Infrastructure.BaseClasses;
using CubeProject.Infrastructure.Enums;
using CubeProject.Infrastructure.Events;
using CubeProject.Infrastructure.Interfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

namespace CubeProject.Modules.Editor.ViewModels
{
    public class FrameViewModel : ViewModelBase, IFrameViewModel
    {
        public FrameViewModel(IUnityContainer container, IEventAggregator aggregator, IDialogService dialogService) : base(container, aggregator)
        {
            _dialogService = dialogService;

            EventAggregator.GetEvent<BrushSizeChangedEvent>().Subscribe(BrushSizeChanged);
            EventAggregator.GetEvent<ShadeChangedEvent>().Subscribe(ShadeChanged);

            EventAggregator.GetEvent<RequestBrushSizeEvent>().Publish(0);
            EventAggregator.GetEvent<RequestShadeEvent>().Publish(0);
        }

        #region Properties
        public RendererSettings Settings
        {
            get { return _settings; }
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
                        ScreenHeight = _frame.Height * (2 + 8),
                        ScreenWidth = _frame.Width * (2 + 8),
                        SizeX = _frame.Width,
                        SizeY = _frame.Height
                    };
                    MatrixRenderer = new MatrixRenderer(_settings);
                }
                OnPropertyChanged();
            }
        }

        public BitmapSource RenderedSource
        {
            get
            {
                if (_renderedSource == null)
                {
                    ReDraw();
                }
                return _renderedSource;
            }
            private set
            {
                _renderedSource = value;
                OnPropertyChanged();
            }
        }

        private MatrixRenderer MatrixRenderer { get; set; }

        public Int16 Duration {
            get
            {
                return Frame.Duration;
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

        #endregion

        #region Private

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

        private PixelCoordinate GetCoordinateFromLocation(Point clickLocation)
        {
            return new PixelCoordinate
            {
                X = (int)clickLocation.X / (Settings.PixelSize + Settings.GapSize),
                Y = (int)clickLocation.Y / (Settings.PixelSize + Settings.GapSize)
            };
        }

        public void ReDraw()
        {
            _renderedSource = MatrixRenderer.Render(_frame.Data, _settings.SizeX, _settings.SizeY);
            OnPropertyChanged("RenderedSource");
        }

        private void TogglePixelAtArea(Point clickLocation, int area, ToggleMode mode, byte shadeLevel)
        {
            if (_frame == null) return;

            PixelCoordinate coordinate = GetCoordinateFromLocation(clickLocation);

            if (area == 1)
            {
                SetPixelAtLocation(mode, shadeLevel, coordinate);
                return;
            }

            // iterate through the selected pixel, and it's surrounding area
            for (int i = (coordinate.X - area / 2); i <= (coordinate.X + area / 2); i++)
            {
                for (int j = (coordinate.Y - area / 2); j <= (coordinate.Y + area / 2); j++)
                {
                    SetPixelAtLocation(mode, shadeLevel, new PixelCoordinate(i,j));
                }
            }
        }

        private void SetPixelAtLocation(ToggleMode mode, byte shadeLevel, PixelCoordinate coordinate)
        {
            // checking boundaries
            if (coordinate.X >= Settings.SizeX || coordinate.Y >= Settings.SizeY || coordinate.X < 0 || coordinate.Y < 0) return;

            switch (mode)
            {
                case ToggleMode.On:
                    _frame[coordinate.X, coordinate.Y] = shadeLevel;
                    break;
                case ToggleMode.Off:
                    _frame[coordinate.X, coordinate.Y] = 0;
                    break;
                case ToggleMode.Inverse:

                    if (_frame[coordinate.X, coordinate.Y] == 0)
                    {
                        _frame[coordinate.X, coordinate.Y] = shadeLevel;
                    }
                    else
                    {
                        _frame[coordinate.X, coordinate.Y] = 0;
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException("mode");
            }
        }

        #region Private State

        private int _brushSize = 0;
        private byte _brushShade = 255;

        private BitmapSource _renderedSource;
        private RendererSettings _settings;
        private IFrame<byte> _frame;
        private DelegateCommand<object> _deleteCommand;
        private DelegateCommand<object> _changeDurationCommand;
        private DelegateCommand<object> _copyCommand;
        private DelegateCommand<object> _pasteCommand;

        private IDialogService _dialogService;

        #endregion
        #endregion

        internal void ReportMouseAtLocation(Point realLocation)
        {
            PixelCoordinate coordinate = GetCoordinateFromLocation(realLocation);
            EventAggregator.GetEvent<StatusBarMessageChangeEvent>().Publish(coordinate.X + " X " + coordinate.Y);
        }

        internal void TurnOnPixelsAtArea(Point realLocation)
        {
            TogglePixelAtArea(realLocation, _brushSize, ToggleMode.On, _brushShade);
            ReDraw();
        }

        internal void TurnOffPixelsAtArea(Point realLocation)
        {
            TogglePixelAtArea(realLocation, _brushSize, ToggleMode.Off, _brushShade);
            ReDraw();
        }
    }
}
