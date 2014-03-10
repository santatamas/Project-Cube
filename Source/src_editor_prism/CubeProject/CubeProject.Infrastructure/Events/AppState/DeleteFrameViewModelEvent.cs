using Microsoft.Practices.Prism.Events;

namespace CubeProject.Infrastructure.Events
{
    /// <summary>
    /// The user requested the deletion of the current frame.
    /// </summary>
    public class DeleteFrameViewModelEvent : CompositePresentationEvent<object>
    {
    }
}
