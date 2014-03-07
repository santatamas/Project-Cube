using System;
using System.Windows;

namespace CubeProject.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();

            string packUri = @"/CubeProject.Shell;component/Themes/Theme.xaml";
            ResourceDictionary theme = Application.LoadComponent(new Uri(packUri, UriKind.Relative)) as ResourceDictionary;
            Application.Current.Resources.MergedDictionaries.Add(theme);
            Application.Current.MainWindow.Resources.MergedDictionaries.Add(theme);
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            
        }
    }
}
