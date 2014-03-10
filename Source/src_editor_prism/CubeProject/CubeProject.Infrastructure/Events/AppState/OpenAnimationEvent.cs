using Microsoft.Practices.Prism.Events;

namespace CubeProject.Infrastructure.Events
{
    /// <summary>
    /// The user requested the opening of an existing animation.
    /// </summary>
    public class OpenAnimationEvent : CompositePresentationEvent<string>
    {
    }
}
