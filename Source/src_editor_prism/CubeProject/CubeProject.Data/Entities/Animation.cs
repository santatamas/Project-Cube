using System.Collections.Generic;
using CubeProject.Infrastructure.Enums;

namespace CubeProject.Data.Entities
{
    /// <summary>
    /// Holds a frameset with a specific <see cref="ColorDepth"/>
    /// </summary>
    /// <seealso cref="CubeProject.Data.Serializers.AnimationSerializer"/>
    public class Animation
    {
        public Animation()
        {
            Frames = new List<Frame<byte>>();
            ColorDepth = ColorDepth.Onebit;
        }

        /// <summary>
        /// Gets or sets the color depth.
        /// </summary>
        /// <value>
        /// The color depth.
        /// </value>
        public ColorDepth ColorDepth { get; set; }

        /// <summary>
        /// Gets or sets the frames.
        /// </summary>
        /// <value>
        /// The frames.
        /// </value>
        public List<Frame<byte>> Frames { get; set; }
    }
}
