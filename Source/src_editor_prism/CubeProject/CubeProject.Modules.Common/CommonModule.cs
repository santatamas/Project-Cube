using CubeProject.Infrastructure.BaseClasses;
using CubeProject.Infrastructure.Interfaces;
using CubeProject.Modules.Common.Services;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace CubeProject.Modules.Common
{
    public class CommonModule : ModuleBase
    {
        public CommonModule(IUnityContainer container, IRegionManager manager) : base(container, manager)
        {
        }

        public override void Initialize()
        {
            this.Container.RegisterType(typeof(IDialogService), typeof(DialogService), "DialogService",
               new ContainerControlledLifetimeManager(), new InjectionConstructor());
            this.Container.RegisterType(typeof(ILoggingService), typeof(LoggingService), "LoggingService",
              new ContainerControlledLifetimeManager(), new InjectionConstructor());
        }
    }
}
