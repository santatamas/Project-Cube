using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CubeProject.Infrastructure.Enums;

namespace CubeProject.Data.Entities
{
    public class Animation
    {
        public Animation()
        {
            Frames = new ObservableCollection<Frame<byte>>();
            FrameDurations = new List<short>();
            ColorDepth = ColorDepth.Onebit;
        }

        public ColorDepth ColorDepth { get; set; }

        public ObservableCollection<Frame<byte>> Frames { get; set; }

        public List<Int16> FrameDurations { get; set; }
    }
}
