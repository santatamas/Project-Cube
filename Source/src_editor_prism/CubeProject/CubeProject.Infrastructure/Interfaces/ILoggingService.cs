namespace CubeProject.Infrastructure.Interfaces
{
    public interface ILoggingService
    {
        void LogInfo(string message);
        void LogError(string message);
        void LogWarning(string message);
    }
}
