using CubeProject.Infrastructure.BaseClasses;
using Microsoft.Practices.Unity;

namespace CubeProject.Modules.Editor.ViewModels
{
    public class AnimationViewModel : ViewModelBase
    {
        private CubeProject.Data.Entities.Animation animation;

        public AnimationViewModel(IUnityContainer container)
            : base(container)
        {
            // TODO: Complete member initialization
            this.animation = animation;
        }

    }
}
