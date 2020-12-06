using System.Reflection;
using Autofac;

namespace TagsCloudContainer
{
    public static class Configurator
    {
        public static IContainer GetContainer()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();
            containerBuilder.RegisterType<TagsCloudCreator>().AsSelf();
            return containerBuilder.Build();
        }
    }
}
