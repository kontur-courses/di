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
            containerBuilder.RegisterType<StopWordsFilter>().AsSelf();
            containerBuilder.RegisterAssemblyTypes(assembly).Where(x => x.Name != "StopWordsFilter").AsImplementedInterfaces();
            containerBuilder.RegisterType<TagsCloudCreator>().AsSelf();
            containerBuilder.RegisterType<FixedColorProvider>().AsImplementedInterfaces();
            return containerBuilder.Build();
        }
    }
}
