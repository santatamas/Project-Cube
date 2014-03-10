using Microsoft.Practices.Prism.Events;

namespace CubeProject.Infrastructure.Events
{
    /// <summary>
    /// The user requested a new Animation.
    /// </summary>
    public class CreateNewAnimationEvent : CompositePresentationEvent<string>
    {
    }
}
