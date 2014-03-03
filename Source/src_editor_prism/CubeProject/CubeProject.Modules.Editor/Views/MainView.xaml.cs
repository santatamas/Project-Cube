using System.Windows;
using System.Windows.Controls;
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
    }
}
