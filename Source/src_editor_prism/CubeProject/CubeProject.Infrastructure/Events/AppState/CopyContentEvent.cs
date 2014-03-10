using Microsoft.Practices.Prism.Events;

namespace CubeProject.Infrastructure.Events
{
    /// <summary>
    /// The current frame's content should be copied.
    /// </summary>
    public class CopyContentEvent : CompositePresentationEvent<object>
    {
    }
}
