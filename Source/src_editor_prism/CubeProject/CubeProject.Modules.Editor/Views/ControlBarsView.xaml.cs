using System.Windows.Controls;
using CubeProject.Infrastructure.Interfaces;
using Microsoft.Practices.Unity;

namespace CubeProject.Modules.Editor.Views
{
    /// <summary>
    /// Interaction logic for ControlBarsView.xaml
    /// </summary>
    public partial class ControlBarsView : UserControl
    {
        [Dependency]
        public IControlBarsViewModel ViewModel
        {
            get { return this.DataContext as IControlBarsViewModel; }
            set { this.DataContext = value; }
        }

        public ControlBarsView()
        {
            InitializeComponent();
        }
    }
}
