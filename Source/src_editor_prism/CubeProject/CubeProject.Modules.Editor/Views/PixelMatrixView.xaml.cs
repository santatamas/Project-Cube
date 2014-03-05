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

        #region EventHandlers
        private void MainScreen_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_viewModel == null) return;

            var realLocation = GetRelativelLocation(e.GetPosition((Image)sender));

            if (e.ChangedButton == MouseButton.Left)
            {
                _viewModel.TurnOnPixelAtLocation(realLocation);
            }

            if (e.ChangedButton == MouseButton.Right)
            {
                _viewModel.TurnOffPixelAtLocation(realLocation);
            }         
        }

        private Point GetRelativelLocation(Point clickLocation)
        {
            var realLocationX = clickLocation.X * (_viewModel.Settings.ScreenWidth / MainScreen.ActualWidth);
            var realLocationY = clickLocation.Y * (_viewModel.Settings.ScreenHeight / MainScreen.ActualHeight);
            var realLocation = new Point(realLocationX, realLocationY);
            return realLocation;
        }

        private void MainScreen_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_viewModel == null) return;

            var realLocation = GetRelativelLocation(e.GetPosition((Image)sender));

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _viewModel.TurnOnPixelAtLocation(realLocation);
            }

            if (e.RightButton == MouseButtonState.Pressed)
            {
                _viewModel.TurnOffPixelAtLocation(realLocation);
            }
        }
        #endregion
    }
}
