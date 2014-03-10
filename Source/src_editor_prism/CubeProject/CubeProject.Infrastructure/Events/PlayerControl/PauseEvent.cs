using Microsoft.Practices.Prism.Events;

namespace CubeProject.Infrastructure.Events
{
    /// <summary>
    /// Pause the current animation.
    /// </summary>
    public class PauseEvent : CompositePresentationEvent<int>
    {
    }
}
