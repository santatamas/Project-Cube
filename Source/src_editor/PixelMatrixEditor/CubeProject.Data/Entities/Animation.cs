using System;
using System.Collections.Generic;
using PixelMatrixEditor.Data;

namespace CubeProject.Data.Entities
{
    public class Animation
    {
        public ColorDepth ColorDepth { get; set; }
        public List<Frame<byte>> Frames { get; set; }
        public List<Int16> FrameDurations { get; set; }
    }
}
