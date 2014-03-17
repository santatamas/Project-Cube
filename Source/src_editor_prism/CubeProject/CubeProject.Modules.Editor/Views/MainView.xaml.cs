using System.Windows.Controls;
using System.Windows.Input;
using CubeProject.Infrastructure.Interfaces;
using Microsoft.Practices.Unity;

namespace CubeProject.Modules.Editor.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        [Dependency]
        public IMainViewModel ViewModel
        {
            get { return this.DataContext as IMainViewModel; }
            set { this.DataContext = value; }
        }
        public MainView()
        {
            InitializeComponent();
        }

        private void UIElement_OnMouseMove(object sender, MouseEventArgs e)
        {
            MatrixView.HandleMouseMove(sender, e);
            FeatureView.HandleMouseMove(sender, e);
        }

        private void UIElement_OnMouseLeave(object sender, MouseEventArgs e)
        {
            FeatureView.HandleMouseLeave(sender, e);
        }

        private void UIElement_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            MatrixView.HandleMouseUp(sender, e);
        }
    }
}
