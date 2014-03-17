using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CubeProject.Graphics;
using CubeProject.Graphics.Renderers;
using CubeProject.Modules.Editor.ViewModels;

namespace CubeProject.Modules.Editor.Views
{
    /// <summary>
    /// Interaction logic for EditorFeatureView.xaml
    /// </summary>
    public partial class EditorFeatureView : UserControl
    {
        public EditorFeatureView()
        {
            InitializeComponent();
            DataContextChanged += EditorFeatureView_DataContextChanged;
        }

        #region Private
        private PixelCoordinate GetCoordinateFromLocation(Point clickLocation)
        {
            return new PixelCoordinate
            {
                X = (int)clickLocation.X / (_viewModel.Settings.PixelSize + _viewModel.Settings.GapSize),
                Y = (int)clickLocation.Y / (_viewModel.Settings.PixelSize + _viewModel.Settings.GapSize)
            };
        }
        private void RenderCursorAtLocation(Point realLocation)
        {
            if (_viewModel == null) return;
            var coordinate = GetCoordinateFromLocation(realLocation);
            if (coordinate.X == _previousCoordinate.X && coordinate.Y == _previousCoordinate.Y) return;
            _previousCoordinate = coordinate;

            ClearCursor();

            if (_viewModel.BrushSize == 1)
            {
                if (coordinate.X >= _viewModel.Settings.SizeX || coordinate.Y >= _viewModel.Settings.SizeY || coordinate.X < 0 || coordinate.Y < 0) return;
                _tempCursorFrame[coordinate.X, coordinate.Y] = 254;
                cursorImage.Source = _cursorRenderer.Render(_tempCursorFrame, _viewModel.Frame.Width, _viewModel.Frame.Height);
            }
            else
            {
                // iterate through the selected pixel, and it's surrounding area
                for (int i = (coordinate.X - _viewModel.BrushSize / 2); i <= (coordinate.X + _viewModel.BrushSize / 2); i++)
                {
                    for (int j = (coordinate.Y - _viewModel.BrushSize / 2); j <= (coordinate.Y + _viewModel.BrushSize / 2); j++)
                    {
                        // checking boundaries
                        if (i >= _viewModel.Settings.SizeX || j >= _viewModel.Settings.SizeY || i < 0 || j < 0) continue;
                        _tempCursorFrame[i, j] = 254;
                    }
                }
                cursorImage.Source = _cursorRenderer.Render(_tempCursorFrame, _viewModel.Frame.Width, _viewModel.Frame.Height);
            }
        }
        private void ClearCursor()
        {
            // Clear temp frame
            for (int i = 0; i < _viewModel.Frame.Width; i++)
            {
                for (int j = 0; j < _viewModel.Frame.Height; j++)
                {
                    _tempCursorFrame[i, j] = 0;
                }
            }
        }
        private void EditorFeatureView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var oldViewModel = e.OldValue as FrameViewModel;
            _viewModel = e.NewValue as FrameViewModel;

            if (_viewModel == null) return;
            if (oldViewModel != null && 
                oldViewModel.Frame.Width == _viewModel.Frame.Width &&
                oldViewModel.Frame.Width == _viewModel.Frame.Height) return;

            _gridRenderer = new GridRenderer(_viewModel.Settings);
            _cursorRenderer = new CursorRenderer(_viewModel.Settings);
            gridImage.Source = _gridRenderer.Render(null, 0, 0);
            _tempCursorFrame = new byte[_viewModel.Frame.Width, _viewModel.Frame.Height];
        }
        private Point GetRelativelLocation(Point clickLocation)
        {
            var realLocationX = clickLocation.X * (_viewModel.Settings.ScreenWidth / gridImage.ActualWidth);
            var realLocationY = clickLocation.Y * (_viewModel.Settings.ScreenHeight / gridImage.ActualHeight);
            var realLocation = new Point(realLocationX, realLocationY);
            return realLocation;
        }

        #region Private State
        private GridRenderer _gridRenderer;
        private FrameViewModel _viewModel;

        private PixelCoordinate _previousCoordinate;
        private byte[,] _tempCursorFrame;
        private CursorRenderer _cursorRenderer;

        #endregion
        #endregion

        #region Public
        public void HandleMouseMove(object sender, MouseEventArgs e)
        {
            if (_viewModel == null)
            {
                e.Handled = false;
                return;
            }

            var realLocation = GetRelativelLocation(e.GetPosition((IInputElement)sender));
            RenderCursorAtLocation(realLocation);
        }

        public void HandleMouseLeave(object sender, MouseEventArgs e)
        {
            if (_viewModel == null)
            {
                e.Handled = false;
                return;
            }
            ClearCursor();
            cursorImage.Source = _cursorRenderer.Render(_tempCursorFrame, _viewModel.Frame.Width, _viewModel.Frame.Height);
        }

        #endregion
    }
}
