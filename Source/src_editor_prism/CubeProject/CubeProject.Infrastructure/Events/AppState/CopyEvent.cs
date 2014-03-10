using Microsoft.Practices.Prism.Events;

namespace CubeProject.Infrastructure.Events
{
    /// <summary>
    /// The current frame (with viewmodel) should be copied.
    /// </summary>
    public class CopyEvent : CompositePresentationEvent<int>
    {
    }
}
