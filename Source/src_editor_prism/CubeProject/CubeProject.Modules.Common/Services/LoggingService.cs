using System.Diagnostics;
using CubeProject.Infrastructure.Enums;
using CubeProject.Infrastructure.Interfaces;

namespace CubeProject.Modules.Common.Services
{
    /// <summary>
    /// Provides WPF implementation for <see cref="ILoggingService"/>.
    /// </summary>
    public class LoggingService : ILoggingService
    {
        public void LogInfo(string message)
        {
            Log(message, LogLevel.Info);
        }

        public void LogError(string message)
        {
            Log(message, LogLevel.Error);
        }

        public void LogWarning(string message)
        {
            Log(message, LogLevel.Warning);
        }

        private void Log(string message, LogLevel level)
        {
            Debug.WriteLine(string.Format("[{0}] {1}", level,message));
        }
    }
}
