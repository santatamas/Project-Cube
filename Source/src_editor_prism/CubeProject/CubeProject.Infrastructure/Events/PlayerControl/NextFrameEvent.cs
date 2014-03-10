using Microsoft.Practices.Prism.Events;

namespace CubeProject.Infrastructure.Events
{
    /// <summary>
    /// Skip to the next frame.
    /// </summary>
    public class NextFrameEvent : CompositePresentationEvent<int>
    {
    }
}
