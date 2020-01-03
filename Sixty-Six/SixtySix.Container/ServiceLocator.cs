using Autofac;
using SixtySix.Container.Modules;

namespace SixtySix.Injection
{
    public static class ServiceLocator
    {
        static IContainer container;
        public static void Build()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<BasicModule>();

            container = builder.Build();
        }

        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}
