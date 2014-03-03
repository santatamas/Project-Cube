using System.Windows.Controls;
using CubeProject.Infrastructure.Interfaces;
using Microsoft.Practices.Unity;

namespace CubeProject.Modules.Editor.Views
{
    /// <summary>
    /// Interaction logic for StatusBarView.xaml
    /// </summary>
    public partial class StatusBarView : UserControl
    {

        [Dependency]
        public IStatusBarViewModel ViewModel
        {
            get { return this.DataContext as IStatusBarViewModel; }
            set { this.DataContext = value; }
        }
        public StatusBarView()
        {
            InitializeComponent();
        }
    }
}
