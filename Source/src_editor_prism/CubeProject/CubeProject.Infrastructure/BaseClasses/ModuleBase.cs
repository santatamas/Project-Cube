using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace CubeProject.Infrastructure.BaseClasses
{
    public abstract class ModuleBase : IModule
    {
        public IRegionManager RegionManager { get; private set; }
        public IUnityContainer Container { get; private set; }

        protected ModuleBase(IUnityContainer container, IRegionManager regionManager)
        {
            RegionManager = regionManager;
            Container = container;
        }

        public abstract void Initialize();
    }
}
