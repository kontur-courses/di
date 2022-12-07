using Autofac;
using TagsCloudVisualisation.App.CloudCreator;
using TagsCloudVisualisation.App.CloudDrawers;
using TagsCloudVisualisation.Clients;
using TagsCloudVisualisation.Clients.ConsoleClient;

namespace TagsCloudVisualisation
{
    static class Program
    {
        static void Main()
        {
            var container = GetContainer();
            using var scope = container.BeginLifetimeScope();
            var client = scope.Resolve<IClient>();
            client.Run();
        }

        private static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleClient>().As<IClient>();
            builder.RegisterType<CloudCreator>().As<ICloudCreator>();
            builder.RegisterType<CloudDrawer>().As<ICloudDrawer>();
            // TODO Переделать зависимости и правильно собрать контейнер
            return builder.Build();
        }
    }
}