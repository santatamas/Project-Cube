using Microsoft.Practices.Prism.Events;

namespace CubeProject.Infrastructure.Events
{
    /// <summary>
    /// The user requested the pasting of the clipboard's content (frame content) to an existing frame.
    /// </summary>
    public class PasteContentEvent : CompositePresentationEvent<object>
    {
    }
}
