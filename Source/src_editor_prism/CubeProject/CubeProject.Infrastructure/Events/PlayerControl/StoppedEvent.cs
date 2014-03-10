using Microsoft.Practices.Prism.Events;

namespace CubeProject.Infrastructure.Events
{
    /// <summary>
    /// The current animation has been stopped.
    /// </summary>
    public class StoppedEvent : CompositePresentationEvent<int>
    {
    }
}
