using System.Windows;

namespace CubeProject.Modules.Editor.Views
{
    /// <summary>
    /// Interaction logic for NewAnimationView.xaml
    /// </summary>
    public partial class NewAnimationView : Window
    {
        public NewAnimationView()
        {
            InitializeComponent();
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Close();
        }
    }
}
