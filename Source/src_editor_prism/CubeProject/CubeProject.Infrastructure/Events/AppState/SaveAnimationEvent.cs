using Microsoft.Practices.Prism.Events;

namespace CubeProject.Infrastructure.Events
{
    /// <summary>
    /// The user requested the saving of the current animation.
    /// </summary>
    public class SaveAnimationEvent : CompositePresentationEvent<string>
    {
    }
}
