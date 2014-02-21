using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CubeProject.Infrastructure.BaseClasses;

namespace PixelMatrixEditor.ViewModels
{
    public class AnimationViewModel : ViewModelBase
    {
        private CubeProject.Data.Entities.Animation animation;

        public AnimationViewModel(CubeProject.Data.Entities.Animation animation)
        {
            // TODO: Complete member initialization
            this.animation = animation;
        }

    }
}
