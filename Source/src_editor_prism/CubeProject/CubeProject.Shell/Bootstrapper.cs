using System.Windows;
using CubeProject.Modules.Common;
using CubeProject.Modules.Editor;
using CubeProject.Shell.View;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;

namespace CubeProject.Shell
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return this.Container.Resolve<ShellView>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            var catalog = new ModuleCatalog();
            catalog.AddModule(typeof(CommonModule));
            catalog.AddModule(typeof(ShellModule));
            catalog.AddModule(typeof(EditorModule));
            return catalog;
        }
    }
}
