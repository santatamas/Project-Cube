using Microsoft.Practices.Prism.Events;

namespace CubeProject.Infrastructure.Events
{
    /// <summary>
    /// The pointer location has been changed.
    /// </summary>
    public class PointerLocationChangedEvent : CompositePresentationEvent<string>
    {
    }
}
