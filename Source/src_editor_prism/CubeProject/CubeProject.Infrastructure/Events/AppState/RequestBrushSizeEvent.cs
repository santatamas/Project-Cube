using Microsoft.Practices.Prism.Events;

namespace CubeProject.Infrastructure.Events
{
    /// <summary>
    /// A viewmodel has requested the currently selected brushsize.
    /// </summary>
    public class RequestBrushSizeEvent : CompositePresentationEvent<int>
    {
    }
}
