using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CubeProject.Data.Entities;
using CubeProject.Graphics.Helpers;
using CubeProject.Graphics.Renderers;
using CubeProject.Infrastructure.Enums;
using PixelMatrixEditor.Annotations;

namespace PixelMatrixEditor.Views
{
    /// <summary>
    /// Interaction logic for PixelMatrixView.xaml
    /// </summary>
    public partial class PixelMatrixView : UserControl, INotifyPropertyChanged
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
            var view = (PixelMatrixView) d;
            var currentFrame = (Frame<byte>) e.NewValue;
            view.MatrixRenderer = new MatrixRenderer(new MatrixInfo
            {
                PixelSize = 8,
                GapSize = 2,
                ScreenHeight = currentFrame.Height * 10,
                ScreenWidth = currentFrame.Width * 10,
                SizeX = currentFrame.Width,
                SizeY = currentFrame.Height
            });
            view.MatrixRenderer.Frame = currentFrame;
        }

        // .NET Property wrapper
        public Frame<byte> MatrixSource
        {
            get { return (Frame<byte>)GetValue(MatrixSourceProperty); }
            set { SetValue(MatrixSourceProperty, value); }
        }
        #endregion

        #region AutoRefresh Dependency Property

        // Dependency Property
        public static readonly DependencyProperty AutoRefreshProperty =
             DependencyProperty.Register("AutoRefresh", typeof(bool),
             typeof(PixelMatrixView), new FrameworkPropertyMetadata(AutoRefreshChanged));

        private static void AutoRefreshChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = (PixelMatrixView)d;
            view.MatrixRenderer.Render();
        }

        // .NET Property wrapper
        public bool AutoRefresh
        {
            get { return (bool)GetValue(MatrixSourceProperty); }
            set { SetValue(MatrixSourceProperty, value); }
        }
        #endregion

        public MatrixRenderer MatrixRenderer
        {
            get { return _matrixRenderer; }
            private set
            {
                _matrixRenderer = value;
                OnPropertyChanged();
            }
        }
        private MatrixRenderer _matrixRenderer;

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
                MatrixRenderer.TogglePixelAtLocation(realLocation, AreaSize - 1, ToggleMode.On);
            }

            if (e.ChangedButton == MouseButton.Right)
            {
                MatrixRenderer.TogglePixelAtLocation(realLocation, AreaSize - 1, ToggleMode.Off);
            }         
        }

        private Point GetRelativelLocation(Point clickLocation)
        {
            var realLocationX = clickLocation.X*(MatrixRenderer.Settings.ScreenWidth/MainScreen.ActualWidth);
            var realLocationY = clickLocation.Y*(MatrixRenderer.Settings.ScreenHeight/MainScreen.ActualHeight);
            Point realLocation = new Point(realLocationX, realLocationY);
            return realLocation;
        }

        private void MainScreen_OnMouseMove(object sender, MouseEventArgs e)
        {
            var realLocation = GetRelativelLocation(e.GetPosition((Image)sender));

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                MatrixRenderer.TogglePixelAtLocation(realLocation, AreaSize - 1, ToggleMode.On);
            }

            if (e.RightButton == MouseButtonState.Pressed)
            {
                MatrixRenderer.TogglePixelAtLocation(realLocation, AreaSize - 1, ToggleMode.Off);
            }
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
