using Microsoft.Practices.Prism.Events;

namespace CubeProject.Infrastructure.Events
{
    /// <summary>
    /// Skip to the previous frame.
    /// </summary>
    public class PreviousFrameEvent : CompositePresentationEvent<int>
    {
    }
}
