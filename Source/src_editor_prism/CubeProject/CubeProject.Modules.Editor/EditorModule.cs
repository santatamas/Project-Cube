using System;
using CubeProject.Infrastructure.BaseClasses;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace CubeProject.Modules.Editor
{
    public class EditorModule : ModuleBase
    {
        public EditorModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager)
        {
        }

        public override void Initialize()
        {

        }
    }
}
