using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CubeProject.Infrastructure.Interfaces;

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
        }

        #region EventHandlers
        private void MainScreen_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            var realLocation = GetRelativelLocation(e.GetPosition((Image)sender));

            if (e.ChangedButton == MouseButton.Left)
            {
                //MatrixRenderer.TogglePixelAtLocation(realLocation, AreaSize - 1, ToggleMode.On, ShadeLevel);
            }

            if (e.ChangedButton == MouseButton.Right)
            {
                //MatrixRenderer.TogglePixelAtLocation(realLocation, AreaSize - 1, ToggleMode.Off, ShadeLevel);
            }         
        }

        private Point GetRelativelLocation(Point clickLocation)
        {
            var realLocationX = 0;//clickLocation.X*(MatrixRenderer.Settings.ScreenWidth/MainScreen.ActualWidth);
            var realLocationY = 0;//clickLocation.Y*(MatrixRenderer.Settings.ScreenHeight/MainScreen.ActualHeight);
            Point realLocation = new Point(realLocationX, realLocationY);
            return realLocation;
        }

        private void MainScreen_OnMouseMove(object sender, MouseEventArgs e)
        {
            var realLocation = GetRelativelLocation(e.GetPosition((Image)sender));

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //MatrixRenderer.TogglePixelAtLocation(realLocation, AreaSize - 1, ToggleMode.On, ShadeLevel);
            }

            if (e.RightButton == MouseButtonState.Pressed)
            {
                //MatrixRenderer.TogglePixelAtLocation(realLocation, AreaSize - 1, ToggleMode.Off, ShadeLevel);
            }
        }
        #endregion
    }
}
