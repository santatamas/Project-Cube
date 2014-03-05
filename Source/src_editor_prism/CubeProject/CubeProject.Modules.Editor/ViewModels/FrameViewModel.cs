using System;
using System.Windows;
using System.Windows.Media.Imaging;
using CubeProject.Data.Entities;
using CubeProject.Graphics;
using CubeProject.Graphics.Renderers;
using CubeProject.Infrastructure.BaseClasses;
using CubeProject.Infrastructure.Enums;
using CubeProject.Infrastructure.Events;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

namespace CubeProject.Modules.Editor.ViewModels
{
    public class FrameViewModel : ViewModelBase
    {
        public FrameViewModel(IUnityContainer container, IEventAggregator aggregator) : base(container, aggregator)
        {
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

        public Frame<byte> Frame
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
        #endregion

        #region Commands
        public DelegateCommand<object> DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new DelegateCommand<object>(Delete)); }
        }

        private void Delete(object obj)
        {
            EventAggregator.GetEvent<DeleteFrameViewModelEvent>().Publish(this);
        }

        #endregion

        #region Private

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

        private void ReDraw()
        {
            _renderedSource = MatrixRenderer.Render(_frame.Data, _settings.SizeX, _settings.SizeY);
            OnPropertyChanged("RenderedSource");
        }

        private void TogglePixelAtLocation(Point clickLocation, int area, ToggleMode mode, byte shadeLevel)
        {
            if (_frame == null) return;

            PixelCoordinate coordinate = GetCoordinateFromLocation(clickLocation);

            // iterate through the selected pixel, and it's surrounding area
            for (int i = (coordinate.X - area); i <= (coordinate.X + area); i++)
            {
                for (int j = (coordinate.Y - area); j <= (coordinate.Y + area); j++)
                {
                    // checking boundaries
                    if (i >= Settings.SizeX || j >= Settings.SizeY || i < 0 || j < 0) continue;

                    // change pixel's value based on the selected drawing mode
                    switch (mode)
                    {
                        case ToggleMode.On:
                            _frame[i, j] = shadeLevel;
                            break;
                        case ToggleMode.Off:
                            _frame[i, j] = 0;
                            break;
                        case ToggleMode.Inverse:

                            if (_frame[i, j] == 0)
                            {
                                _frame[i, j] = shadeLevel;
                            }
                            else
                            {
                                _frame[i, j] = 0;
                            }

                            break;
                        default:
                            throw new ArgumentOutOfRangeException("mode");
                    }
                }
            }
        }


        #region Private State

        private int _brushSize = 0;
        private byte _brushShade = 255;

        private BitmapSource _renderedSource;
        private RendererSettings _settings;
        private Frame<byte> _frame;
        private DelegateCommand<object> _deleteCommand;
        #endregion
        #endregion

        internal void TurnOnPixelAtLocation(Point realLocation)
        {
            TogglePixelAtLocation(realLocation, _brushSize, ToggleMode.On, _brushShade);
            ReDraw();
        }

        internal void TurnOffPixelAtLocation(Point realLocation)
        {
            TogglePixelAtLocation(realLocation, _brushSize, ToggleMode.Off, _brushShade);
            ReDraw();
        }
    }
}
