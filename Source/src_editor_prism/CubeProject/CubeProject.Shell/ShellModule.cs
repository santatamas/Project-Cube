using CubeProject.Infrastructure.BaseClasses;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace CubeProject.Shell
{
    public class ShellModule : ModuleBase
    {
        public ShellModule(IUnityContainer container, IRegionManager regionManager)
            : base(container, regionManager)
        {
        }

        public override void Initialize()
        {
            
        }
    }
}
