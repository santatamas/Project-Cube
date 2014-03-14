using Microsoft.Practices.Prism.Events;

namespace CubeProject.Infrastructure.Events
{
    /// <summary>
    /// The state of the gridvisibility supposed to change..0
    /// </summary>
    public class ToggleGridVisibilityEvent : CompositePresentationEvent<bool>
    {
    }
}
