using System.Collections.Generic;
using System.Windows.Documents;
using CubeProject.Infrastructure.BaseClasses;
using CubeProject.Infrastructure.Enums;

namespace PixelMatrixEditor.ViewModels
{
    public class NewAnimationViewModel : ViewModelBase
    {
        public NewAnimationViewModel()
        {
            _avaliableColorDepths = new List<ColorDepth>
            {
                ColorDepth.Onebit,
                ColorDepth.GrayScale
            };
            _selectedColorDepth = _avaliableColorDepths[0];
        }

        private List<ColorDepth> _avaliableColorDepths;
        private ColorDepth _selectedColorDepth;
        private short _frameWidth;
        private short _frameHeight;

        public List<ColorDepth> AvaliableColorDepths
        {
            get { return _avaliableColorDepths; }
            set
            {
                _avaliableColorDepths = value;
                OnPropertyChanged();
            }
        }

        public ColorDepth SelectedColorDepth
        {
            get { return _selectedColorDepth; }
            set
            {
                _selectedColorDepth = value;
                OnPropertyChanged();
            }
        }

        public short FrameWidth
        {
            get { return _frameWidth; }
            set
            {
                _frameWidth = value;
                OnPropertyChanged();
            }
        }
        public short FrameHeight
        {
            get { return _frameHeight; }
            set
            {
                _frameHeight = value;
                OnPropertyChanged();
            }
        }
    }
}
