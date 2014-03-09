using System.Collections.Generic;
using CubeProject.Infrastructure.Enums;

namespace CubeProject.Data.Entities
{
    public class Animation
    {
        public Animation()
        {
            Frames = new List<Frame<byte>>();
            ColorDepth = ColorDepth.Onebit;
        }

        public ColorDepth ColorDepth { get; set; }

        public List<Frame<byte>> Frames { get; set; }
    }
}
