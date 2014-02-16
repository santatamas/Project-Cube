using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PixelMatrixEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MatrixRenderer MatrixRenderer { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            MatrixRenderer = new MatrixRenderer(new MatrixInfo
            {
                PixelSize = 8,
                GapSize = 2,
                ScreenHeight = (int)MainScreen.Height,
                ScreenWidth = (int)MainScreen.Width,
                SizeX = 72,
                SizeY = 72
            });

            this.DataContext = this;
        }

        private void MainScreen_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            Point clickLocation = e.GetPosition((Image)sender);
            MatrixRenderer.TogglePixelAtLocation(clickLocation);
        }


        private void MainScreen_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point clickLocation = e.GetPosition((Image)sender);
                MatrixRenderer.TogglePixelAtLocation(clickLocation);
            }
        }
    }
}
