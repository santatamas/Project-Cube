using System;
using System.Collections.Generic;
using CubeProject.Infrastructure.BaseClasses;
using CubeProject.Infrastructure.Enums;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

namespace CubeProject.Modules.Editor.ViewModels
{
    public class NewAnimationViewModel : DialogViewModelBase
    {
        public NewAnimationViewModel(IUnityContainer container, IEventAggregator aggregator)
            : base(container, aggregator)
        {
            _avaliableColorDepths = new List<ColorDepth>
            {
                ColorDepth.Onebit,
                ColorDepth.GrayScale
            };
            _selectedColorDepth = _avaliableColorDepths[1];
        }

        private List<ColorDepth> _avaliableColorDepths;
        private ColorDepth _selectedColorDepth;
        private short _frameWidth = 72;
        private short _frameHeight = 72;

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
                if (!ValidateRange(_frameWidth, 0, 100, "FrameWidth"))
                {
                    RemoveError("FrameWidth");
                }
                OnPropertyChanged();
            }
        }
        public short FrameHeight
        {
            get { return _frameHeight; }
            set
            {
                _frameHeight = value;
                if (!ValidateRange(_frameHeight, 0, 100, "FrameHeight"))
                {
                    RemoveError("FrameHeight");
                }
                OnPropertyChanged();
            }
        }

        private bool ValidateRange(short valueToValidate, short minValue, short maxValue, string propertyName)
        {
            bool result = false;
            if (valueToValidate <= minValue)
            {
                AddError(propertyName, String.Format("Width cannot be smaller or equal to {0}!", minValue));
                result = true;
            }
            if (valueToValidate > maxValue)
            {
                AddError(propertyName, String.Format("Width cannot be grater than {0}!", maxValue));
                result = true;
            }
            return result;
        }
    }
}
