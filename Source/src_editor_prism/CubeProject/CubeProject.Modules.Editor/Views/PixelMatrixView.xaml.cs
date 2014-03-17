using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CubeProject.Modules.Editor.ViewModels;

namespace CubeProject.Modules.Editor.Views
{
    /// <summary>
    /// Interaction logic for PixelMatrixView.xaml
    /// </summary>
    public partial class PixelMatrixView : UserControl
    {
        public bool CanDraw
        {
            get { return _canDraw; }
            set { _canDraw = value; }
        }

        public PixelMatrixView()
        {
            InitializeComponent();
            DataContextChanged += PixelMatrixView_DataContextChanged;
        }

        void PixelMatrixView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _viewModel = e.NewValue as FrameViewModel;
        }

        private FrameViewModel _viewModel;
        private bool _canDraw = true;

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
                _viewModel.TurnOnPixelsAtArea(realLocation);
            }

            if (e.ChangedButton == MouseButton.Right)
            {
                _viewModel.TurnOffPixelsAtArea(realLocation);
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
                _viewModel.TurnOnPixelsAtArea(realLocation);
            }

            if (e.RightButton == MouseButtonState.Pressed)
            {
                _viewModel.TurnOffPixelsAtArea(realLocation);
            }
        }
        #endregion
    }
}
