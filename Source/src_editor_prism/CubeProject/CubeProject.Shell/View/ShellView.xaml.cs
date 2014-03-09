using System.Reflection;
using System.Windows;
using CubeProject.Modules.Editor.ViewModels;
using Microsoft.Practices.Unity;

namespace CubeProject.Shell.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        [Dependency]
        public ShellViewModel ViewModel
        {
            get { return this.DataContext as ShellViewModel; }
            set { this.DataContext = value; }
        }

        public ShellView()
        {
            InitializeComponent();
            this.Title = "PixelMatrix Editor - " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}
