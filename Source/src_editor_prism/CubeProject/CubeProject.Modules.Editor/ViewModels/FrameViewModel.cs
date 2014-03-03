using System;
using System.Windows;
using System.Windows.Media.Imaging;
using CubeProject.Data.Entities;
using CubeProject.Graphics;
using CubeProject.Graphics.Renderers;
using CubeProject.Infrastructure.BaseClasses;
using CubeProject.Infrastructure.Enums;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

namespace CubeProject.Modules.Editor.ViewModels
{
    public class FrameViewModel : ViewModelBase
    {
        private Frame<byte> _frame;
        private DelegateCommand<object> _deleteCommand;

        public Frame<byte> Frame
        {
            get { return _frame; }
            set
            {
                _frame = value;
                OnPropertyChanged();
            }
        }

        public FrameViewModel(IUnityContainer container, IEventAggregator aggregator) : base(container, aggregator)
        {
        }

        private BitmapSource _renderedSource;
        private RendererSettings _settings;

        public BitmapSource RenderedSource
        {
            get
            {
                return _renderedSource;
            }
        }

        private MatrixRenderer MatrixRenderer { get; set; }

        #region Commands
        public DelegateCommand<object> DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new DelegateCommand<object>(Delete)); }
        }

        private void Delete(object obj)
        {
            
        }

        #endregion

        public RendererSettings Settings
        {
            get { return _settings; }
        }

        private PixelCoordinate GetCoordinateFromLocation(Point clickLocation)
        {
            return new PixelCoordinate
            {
                X = (int)clickLocation.X / (Settings.PixelSize + Settings.GapSize),
                Y = (int)clickLocation.Y / (Settings.PixelSize + Settings.GapSize)
            };
        }

        #region Public

        public void TogglePixelAtLocation(Point clickLocation, int area, ToggleMode mode)
        {
            TogglePixelAtLocation(clickLocation, area, mode, 50);
        }

        public void TogglePixelAtLocation(Point clickLocation, int area, ToggleMode mode, byte shadeLevel)
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
            //Render();
        }
        #endregion
    }
}
