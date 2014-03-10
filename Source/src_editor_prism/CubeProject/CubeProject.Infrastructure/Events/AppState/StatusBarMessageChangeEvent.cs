using Microsoft.Practices.Prism.Events;

namespace CubeProject.Infrastructure.Events
{
    /// <summary>
    /// A viewmodel requested to display a new statusbar message.
    /// </summary>
    public class StatusBarMessageChangeEvent : CompositePresentationEvent<string>
    {
    }
}
