using System.Windows;
using PixelMatrixEditor.ViewModels;

namespace PixelMatrixEditor.Views
{
    /// <summary>
    /// Interaction logic for NewAnimationView.xaml
    /// </summary>
    public partial class NewAnimationView : Window
    {
        public NewAnimationView()
        {
            InitializeComponent();
            this.DataContext = new NewAnimationViewModel();
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Close();
        }
    }
}
