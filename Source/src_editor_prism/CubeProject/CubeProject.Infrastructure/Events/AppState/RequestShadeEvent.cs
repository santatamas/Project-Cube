using Microsoft.Practices.Prism.Events;

namespace CubeProject.Infrastructure.Events
{
    /// <summary>
    /// A viewmodel has requested the currently selected shade.
    /// </summary>
    public class RequestShadeEvent : CompositePresentationEvent<byte>
    {
    }
}
