using System.Windows.Controls;
using CubeProject.Infrastructure.Interfaces;
using Microsoft.Practices.Unity;

namespace CubeProject.Modules.Editor.Views
{
    /// <summary>
    /// Interaction logic for PlayerControlView.xaml
    /// </summary>
    public partial class PlayerControlView : UserControl
    {
        [Dependency]
        public IPlayerControlViewModel ViewModel
        {
            get { return this.DataContext as IPlayerControlViewModel; }
            set { this.DataContext = value; }
        }
        public PlayerControlView()
        {
            InitializeComponent();
        }
    }
}
