using Microsoft.Practices.Prism.Events;

namespace CubeProject.Infrastructure.Events
{
    /// <summary>
    /// The selected shade has been changed.
    /// </summary>
    public class ShadeChangedEvent : CompositePresentationEvent<byte>
    {
    }
}
