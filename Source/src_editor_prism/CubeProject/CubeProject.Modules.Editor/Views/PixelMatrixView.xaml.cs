using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CubeProject.Graphics;
using CubeProject.Graphics.Renderers;
using CubeProject.Infrastructure.Enums;
using CubeProject.Modules.Editor.ViewModels;

namespace CubeProject.Modules.Editor.Views
{
    /// <summary>
    /// Interaction logic for PixelMatrixView.xaml
    /// </summary>
    public partial class PixelMatrixView : UserControl, IDisposable
    {
        public static int InstanceCount = 0;
        public bool CanDraw
        {
            get { return _canDraw; }
            set { _canDraw = value; }
        }

        public PixelMatrixView()
        {
            InitializeComponent();
            DataContextChanged += PixelMatrixView_DataContextChanged;
            InstanceCount++;
            Debug.WriteLine("PixelMatrixView instance count: " + InstanceCount);
        }

        ~PixelMatrixView()
        {
            Dispose();
            InstanceCount--;
        }

        void PixelMatrixView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var oldViewModel = e.OldValue as FrameViewModel;
            _viewModel = e.NewValue as FrameViewModel;

            if (oldViewModel != null)
                oldViewModel.FrameChanged -= _viewModel_FrameChanged;


            if (_viewModel == null) return;
            _viewModel.FrameChanged += _viewModel_FrameChanged;

            if (oldViewModel == null ||
                oldViewModel.Frame.Width != _viewModel.Frame.Width ||
                oldViewModel.Frame.Width != _viewModel.Frame.Height)
            {
                if (_matrixRenderer != null) _matrixRenderer.Dispose();

                _matrixRenderer = new MatrixRenderer(_viewModel.Settings);
                
            }
            DisplaySnapshot();
            Debug.WriteLine("frame index: " + _viewModel.Index);
        }

        private void _viewModel_FrameChanged(object sender, EventArgs e)
        {
            MainScreen.Source = _matrixRenderer.Render(_viewModel.Frame.Data, _viewModel.Settings.SizeX, _viewModel.Settings.SizeY);
        }

        private FrameViewModel _viewModel;
        private bool _canDraw = true;
        private MatrixRenderer _matrixRenderer;
        private object _lockObject = new object();

        #region EventHandlers
        public void HandleMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_viewModel == null || !CanDraw)
            {
                e.Handled = false;
                return;
            }

            var realLocation = GetRelativelLocation(e.GetPosition((IInputElement)sender));

            if (e.ChangedButton == MouseButton.Left)
            {
                TurnOnPixelsAtArea(realLocation);
            }

            if (e.ChangedButton == MouseButton.Right)
            {
                TurnOffPixelsAtArea(realLocation);
            }         
        }

        private Point GetRelativelLocation(Point clickLocation)
        {
            var realLocationX = clickLocation.X * (_viewModel.Settings.ScreenWidth / MainScreen.ActualWidth);
            var realLocationY = clickLocation.Y * (_viewModel.Settings.ScreenHeight / MainScreen.ActualHeight);
            var realLocation = new Point(realLocationX, realLocationY);
            return realLocation;
        }

        public void HandleMouseMove(object sender, MouseEventArgs e)
        {
            if (_viewModel == null || !CanDraw)
            {
                e.Handled = false;
                return;
            }

            var realLocation = GetRelativelLocation(e.GetPosition((IInputElement)sender));

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                TurnOnPixelsAtArea(realLocation);
            }

            if (e.RightButton == MouseButtonState.Pressed)
            {
                TurnOffPixelsAtArea(realLocation);
            }
        }

        private PixelCoordinate GetCoordinateFromLocation(Point clickLocation)
        {
            return new PixelCoordinate
            {
                X = (int)clickLocation.X / (_viewModel.Settings.PixelSize + _viewModel.Settings.GapSize),
                Y = (int)clickLocation.Y / (_viewModel.Settings.PixelSize + _viewModel.Settings.GapSize)
            };
        }

        public void DisplaySnapshot()
        {
            MainScreen.Source = null;
            MainScreen.Source = _matrixRenderer.Render(_viewModel.Frame.Data, _viewModel.Settings.SizeX, _viewModel.Settings.SizeY).CloneCurrentValue();
        }

        private void TogglePixelAtArea(Point clickLocation, int area, ToggleMode mode, byte shadeLevel)
        {
            if (_viewModel.Frame == null) return;

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
                    SetPixelAtLocation(mode, shadeLevel, new PixelCoordinate(i, j));
                }
            }
        }

        private void SetPixelAtLocation(ToggleMode mode, byte shadeLevel, PixelCoordinate coordinate)
        {
            // checking boundaries
            if (coordinate.X >= _viewModel.Settings.SizeX || coordinate.Y >= _viewModel.Settings.SizeY || coordinate.X < 0 || coordinate.Y < 0) return;

            //switch (mode)
            //{
            //    case ToggleMode.On:
            //        _viewModel.Frame.Data[coordinate.X, coordinate.Y] = shadeLevel;
            //        break;
            //    case ToggleMode.Off:
            //        _viewModel.Frame.Data[coordinate.X, coordinate.Y] = 0;
            //        break;
            //    case ToggleMode.Inverse:

            //        if (_viewModel.Frame.Data[coordinate.X, coordinate.Y] == 0)
            //        {
            //            _viewModel.Frame.Data[coordinate.X, coordinate.Y] = shadeLevel;
            //        }
            //        else
            //        {
            //            _viewModel.Frame.Data[coordinate.X, coordinate.Y] = 0;
            //        }

            //        break;
            //    default:
            //        throw new ArgumentOutOfRangeException("mode");
            //}
            _viewModel.InvokeOnFrameChanged();
        }
        #endregion

        internal void TurnOnPixelsAtArea(Point realLocation)
        {
            TogglePixelAtArea(realLocation, _viewModel.BrushSize, ToggleMode.On, _viewModel.BrushShade);
        }

        internal void TurnOffPixelsAtArea(Point realLocation)
        {
            TogglePixelAtArea(realLocation, _viewModel.BrushSize, ToggleMode.Off, _viewModel.BrushShade);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            lock (LockObject)
            {
                if (disposing)
                {
                    if (_matrixRenderer != null)
                    {
                        _matrixRenderer.Dispose();
                        _matrixRenderer = null;
                    }
                    if(_viewModel != null)
                        _viewModel.FrameChanged -= _viewModel_FrameChanged;
                }
            }
        }

        private object LockObject
        {
            get { return _lockObject; }
            set { _lockObject = value; }
        }
    }
}
