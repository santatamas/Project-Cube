using System.Windows;
using System.Windows.Controls;
using CubeProject.Data.Entities;
using PixelMatrixEditor.ViewModels;

namespace PixelMatrixEditor.Views
{
    /// <summary>
    /// Interaction logic for AnimationView.xaml
    /// </summary>
    public partial class AnimationView : UserControl
    {
        //#region AreaSize Dependency Property

        //// Dependency Property
        //public static readonly DependencyProperty AnimationProperty =
        //     DependencyProperty.Register("Animation", typeof(Animation),
        //     typeof(AnimationView), new FrameworkPropertyMetadata(AnimationChanged));

        //private static void AnimationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var view = (AnimationView) d;
        //    var animation = (Animation) e.NewValue;
        //    view.DataContext = new AnimationViewModel(animation);
        //}

        //// .NET Property wrapper
        //public Animation Animation
        //{
        //    get { return (Animation)GetValue(AnimationProperty); }
        //    set { SetValue(AnimationProperty, value); }
        //}
        //#endregion

        public AnimationView()
        {
            InitializeComponent();
        }
    }
}
