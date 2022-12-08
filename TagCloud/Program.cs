using Autofac;
using TagCloud.App.CloudCreatorDriver.CloudCreator;
using TagCloud.App.CloudCreatorDriver.CloudDrawers;
using TagCloud.Clients;
using TagCloud.Clients.ConsoleClient;
namespace TagCloud
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