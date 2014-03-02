using CubeProject.Infrastructure.Interfaces;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace CubeProject.Modules.Logging
{
    public class LoggingModule : IModule
    {
        private IUnityContainer _container;
        public LoggingModule(IUnityContainer container)
        {
            _container = container;
        }
        public void Initialize()
        {
            _container.RegisterType(typeof (ILoggingService), typeof (LoggingService), "LoggingService",
                new ContainerControlledLifetimeManager(), null);
        }
    }
}
