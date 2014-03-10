namespace CubeProject.Infrastructure.Interfaces
{
    /// <summary>
    /// Capable to log a message at different log levels.
    /// </summary>
    public interface ILoggingService
    {
        void LogInfo(string message);
        void LogError(string message);
        void LogWarning(string message);
    }
}
