using CubeProject.Infrastructure.Enums;
using Microsoft.Practices.Prism.Events;

namespace CubeProject.Infrastructure.Events
{
    /// <summary>
    /// Indicates that the <see cref="ColorDepth"/> of the current animation/frame has been changed.
    /// </summary>
    /// <seealso cref="ColorDepth"/>
    public class ColorDepthChangedEvent : CompositePresentationEvent<ColorDepth>
    {
    }
}
