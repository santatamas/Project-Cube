using Microsoft.Practices.Prism.Events;

namespace CubeProject.Infrastructure.Events
{
    /// <summary>
    /// The user requested the saving of the current animation as a new file.
    /// </summary>
    public class SaveAnimationAsEvent : CompositePresentationEvent<string>
    {
    }
}
