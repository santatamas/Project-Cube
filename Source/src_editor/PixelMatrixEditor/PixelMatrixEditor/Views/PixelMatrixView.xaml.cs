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
                SizeY = currentFrame.Height,
                ColorDepth = view.RenderDepth
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

        #region RenderDepth Dependency Property

        // Dependency Property
        public static readonly DependencyProperty RenderDepthProperty =
             DependencyProperty.Register("RenderDepth", typeof(ColorDepth),
             typeof(PixelMatrixView), new FrameworkPropertyMetadata(RenderDepthChanged));

        private static void RenderDepthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = (PixelMatrixView)d;
            if (e.NewValue != null && e.NewValue != DependencyProperty.UnsetValue)
            {
                var currentDepth = (ColorDepth) e.NewValue;
                view.MatrixRenderer.Settings.ColorDepth = currentDepth;
            }
        }


        // .NET Property wrapper
        public ColorDepth RenderDepth
        {
            get { return (ColorDepth)GetValue(RenderDepthProperty); }
            set { SetValue(RenderDepthProperty, value); }
        }
        #endregion

        #region ShadeLevel Dependency Property

        // Dependency Property
        public static readonly DependencyProperty ShadeLevelProperty =
             DependencyProperty.Register("ShadeLevel", typeof(byte),
             typeof(PixelMatrixView), new FrameworkPropertyMetadata(null));


        // .NET Property wrapper
        public byte ShadeLevel
        {
            get { return (byte)GetValue(ShadeLevelProperty); }
            set { SetValue(ShadeLevelProperty, value); }
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
                MatrixRenderer.TogglePixelAtLocation(realLocation, AreaSize - 1, ToggleMode.On, ShadeLevel);
            }

            if (e.ChangedButton == MouseButton.Right)
            {
                MatrixRenderer.TogglePixelAtLocation(realLocation, AreaSize - 1, ToggleMode.Off, ShadeLevel);
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
                MatrixRenderer.TogglePixelAtLocation(realLocation, AreaSize - 1, ToggleMode.On, ShadeLevel);
            }

            if (e.RightButton == MouseButtonState.Pressed)
            {
                MatrixRenderer.TogglePixelAtLocation(realLocation, AreaSize - 1, ToggleMode.Off, ShadeLevel);
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
