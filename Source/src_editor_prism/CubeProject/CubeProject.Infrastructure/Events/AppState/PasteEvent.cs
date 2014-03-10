using Microsoft.Practices.Prism.Events;

namespace CubeProject.Infrastructure.Events
{
    /// <summary>
    /// The user requested the pasting of the clipboard's content (frame with viewmodel) as a new frame.
    /// </summary>
    public class PasteEvent : CompositePresentationEvent<int>
    {
    }
}
