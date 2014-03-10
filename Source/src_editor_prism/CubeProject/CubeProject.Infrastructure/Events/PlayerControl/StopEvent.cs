using Microsoft.Practices.Prism.Events;

namespace CubeProject.Infrastructure.Events
{
    /// <summary>
    /// Stop the current animation.
    /// </summary>
    public class StopEvent : CompositePresentationEvent<int>
    {
    }
}
