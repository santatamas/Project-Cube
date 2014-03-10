using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace CubeProject.Infrastructure.BaseClasses
{
    /// <summary>
    /// Default implementation of the <see cref="IModule"/> interface, extended with 
    /// RegionManager, and Container properties. 
    /// </summary>
    public abstract class ModuleBase : IModule
    {
        /// <summary>
        /// Gets the injected region manager.
        /// </summary>
        /// <value>
        /// The region manager.
        /// </value>
        public IRegionManager RegionManager { get; private set; }
        /// <summary>
        /// Gets the injected UnityContainer.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public IUnityContainer Container { get; private set; }

        protected ModuleBase(IUnityContainer container, IRegionManager regionManager)
        {
            RegionManager = regionManager;
            Container = container;
        }

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public abstract void Initialize();
    }
}
