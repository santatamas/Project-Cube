using Microsoft.Practices.Prism.Events;

namespace CubeProject.Infrastructure.Events
{
    /// <summary>
    /// Start or restart the animation.
    /// </summary>
    public class PlayEvent : CompositePresentationEvent<int>
    {
    }
}
