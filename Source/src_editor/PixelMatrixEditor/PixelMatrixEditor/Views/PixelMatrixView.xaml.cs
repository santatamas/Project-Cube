using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CubeProject.Data.Entities;
using CubeProject.Graphics.Helpers;
using CubeProject.Graphics.Renderers;
using PixelMatrixEditor.Models;

namespace PixelMatrixEditor.Views
{
    /// <summary>
    /// Interaction logic for PixelMatrixView.xaml
    /// </summary>
    public partial class PixelMatrixView : UserControl
    {
        #region AreaSize Dependency Property

        // Dependency Property
        public static readonly DependencyProperty AreaSizeProperty =
             DependencyProperty.Register("AreaSize", typeof(int),
             typeof(PixelMatrixView), new FrameworkPropertyMetadata(0));

        // .NET Property wrapper
        public int AreaSize
        {
            get { return (int)GetValue(AreaSizeProperty); }
            set { SetValue(AreaSizeProperty, value); }
        }
        #endregion

        #region MatrixSource Dependency Property

        // Dependency Property
        public static readonly DependencyProperty MatrixSourceProperty =
             DependencyProperty.Register("MatrixSource", typeof(Frame<byte>),
             typeof(PixelMatrixView), new FrameworkPropertyMetadata(MatrixSourceChanged));

        private static void MatrixSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PixelMatrixView view = (PixelMatrixView) d;
            view.UpdateRenderer((Frame<byte>)e.NewValue);
        }

        // .NET Property wrapper
        public Frame<byte> MatrixSource
        {
            get { return (Frame<byte>)GetValue(MatrixSourceProperty); }
            set { SetValue(MatrixSourceProperty, value); }
        }
        #endregion

        public MatrixRenderer MatrixRenderer { get; private set; }
        public PixelMatrixView()
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
        }

        #region EventHandlers
        private void MainScreen_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            Point clickLocation = e.GetPosition((Image)sender);
            if (e.ChangedButton == MouseButton.Left)
            {
                MatrixRenderer.TogglePixelAtLocation(clickLocation, AreaSize - 1, ToggleMode.On);
            }

            if (e.ChangedButton == MouseButton.Right)
            {
                MatrixRenderer.TogglePixelAtLocation(clickLocation, AreaSize - 1, ToggleMode.Off);
            }
        }
        private void MainScreen_OnMouseMove(object sender, MouseEventArgs e)
        {
            Point clickLocation = e.GetPosition((Image)sender);

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                MatrixRenderer.TogglePixelAtLocation(clickLocation, AreaSize - 1, ToggleMode.On);
            }

            if (e.RightButton == MouseButtonState.Pressed)
            {
                MatrixRenderer.TogglePixelAtLocation(clickLocation, AreaSize - 1, ToggleMode.Off);
            }
        }
        #endregion
        private void UpdateRenderer(Frame<byte> matrix)
        {
            MatrixRenderer.Matrix = matrix;
        }
    }
}
