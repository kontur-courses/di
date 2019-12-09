using Autofac;
using TagsCloudGenerator.Interfaces;
using TagsCloudGeneratorExtensions;

namespace TagsCloudGenerator_Tests
{
    internal class SingletonScopeInstancesContainer
    {
        private readonly IContainer container;

        public SingletonScopeInstancesContainer() => container = CreateContainer();

        public Type Get<Type>() => container.Resolve<Type>();

        private IContainer CreateContainer()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder
                .RegisterAssemblyTypes(typeof(ISettings).Assembly, typeof(Settings).Assembly)
                .Where(t => t != typeof(ISettings) && t != typeof(Settings))
                .AsImplementedInterfaces()
                .AsSelf()
                .SingleInstance();
            containerBuilder.RegisterType<Settings>().As<ISettings>().AsSelf().SingleInstance();
            return containerBuilder.Build();
        }
    }
}