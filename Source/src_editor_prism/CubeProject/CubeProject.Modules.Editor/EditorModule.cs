using CubeProject.Data.Entities;
using CubeProject.Infrastructure.BaseClasses;
using CubeProject.Infrastructure.Constants;
using CubeProject.Infrastructure.Interfaces;
using CubeProject.Modules.Editor.ViewModels;
using CubeProject.Modules.Editor.Views;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace CubeProject.Modules.Editor
{
    public class EditorModule : ModuleBase
    {
        public EditorModule(IUnityContainer container, IRegionManager regionManager)
            : base(container, regionManager)
        {
        }

        public override void Initialize()
        {
            Container.RegisterType<IFrameViewModel<PixelColor>, FrameViewModel>(new InjectionConstructor(typeof(IUnityContainer), typeof(IEventAggregator), typeof(IDialogService)));
            Container.RegisterType<IShellViewModel, ShellViewModel>(new InjectionConstructor(typeof(IUnityContainer), typeof(IEventAggregator)));
            Container.RegisterType<IChangeDurationViewModel, ChangeDurationViewModel>(new InjectionConstructor(typeof(IUnityContainer), typeof(IEventAggregator)));
            Container.RegisterType<IPlayerControlViewModel, PlayerControlViewModel>(new InjectionConstructor(typeof(IUnityContainer), typeof(IEventAggregator)));
            Container.RegisterType<IControlBarsViewModel, ControlBarsViewModel>(new InjectionConstructor(typeof(IUnityContainer), typeof(IEventAggregator)));
            Container.RegisterType<IMainViewModel, MainViewModel>(new InjectionConstructor(typeof(IUnityContainer), typeof(IEventAggregator)));
            Container.RegisterType<IStatusBarViewModel, StatusBarViewModel>(new InjectionConstructor(typeof(IUnityContainer), typeof(IEventAggregator)));

            RegionManager.Regions[RegionNames.ToolbarRegion].Add(Container.Resolve<ControlBarsView>());
            RegionManager.Regions[RegionNames.MainRegion].Add(Container.Resolve<MainView>());
            RegionManager.Regions[RegionNames.StatusbarRegion].Add(Container.Resolve<StatusBarView>());
            RegionManager.Regions[RegionNames.PlayerControlRegion].Add(Container.Resolve<PlayerControlView>());
        }
    }
}
