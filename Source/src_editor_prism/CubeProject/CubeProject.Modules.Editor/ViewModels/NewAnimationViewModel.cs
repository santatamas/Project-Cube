using System;
using System.Collections.Generic;
using CubeProject.Infrastructure.BaseClasses;
using CubeProject.Infrastructure.Enums;
using CubeProject.Infrastructure.Interfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

namespace CubeProject.Modules.Editor.ViewModels
{
    public class NewAnimationViewModel : ViewModelBase, IDialogResultProvider, IDialogViewModel
    {
        public NewAnimationViewModel(IUnityContainer container, IEventAggregator aggregator)
            : base(container, aggregator)
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
        private DelegateCommand<object> _okCommand;

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

        public DelegateCommand<object> OkCommand
        {
            get { return _okCommand ?? (_okCommand = new DelegateCommand<object>(Ok)); }
        }

        private void Ok(object obj)
        {
            _dialogResult = this;

            if(OkTriggered != null)
                OkTriggered(this, new EventArgs());

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

        private object _dialogResult;
        public object DialogResult
        {
            get
            {
                return _dialogResult;
            }
        }

        public event System.EventHandler OkTriggered;
    }
}
