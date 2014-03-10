using Microsoft.Practices.Prism.Events;

namespace CubeProject.Infrastructure.Events
{
    /// <summary>
    /// The selected brushsize has been changed.
    /// </summary>
    public class BrushSizeChangedEvent : CompositePresentationEvent<int>
    {
    }
}
