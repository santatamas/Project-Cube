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
            Container.RegisterType<IDialogService, DialogService>();
            Container.RegisterType<ILoggingService, LoggingService>();
        }
    }
}
