using Microsoft.Practices.Prism.Events;

namespace CubeProject.Infrastructure.Events
{
    /// <summary>
    /// Indicates that a request to close the application is in progress.
    /// </summary>
    public class CloseApplicationEvent : CompositePresentationEvent<string>
    {
    }
}
