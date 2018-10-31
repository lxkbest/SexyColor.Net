using Autofac;
using Autofac.Core;

namespace SexyColor.Infrastructure
{
    public class DIContainer
    {
        private static IContainer _container;

        public static void RegisterContainer(IContainer container)
        {
            _container = container;
        }

        public static T Resolve<T>()
        {
            using (ILifetimeScope scope = _container.BeginLifetimeScope())
            {
                return scope.Resolve<T>();
            }
        }

        public static T Resolve<T>(params Parameter[] parameters)
        {
            using (_container.BeginLifetimeScope())
            {
                return _container.Resolve<T>(parameters);
            }
        }
    }
}
