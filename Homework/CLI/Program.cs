using Autofac;
using TagsCloudContainer;
using TagsCloudContainer.Client;
using TagsCloudContainer.ContainerConfigurers;

namespace CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(new Client(args)).As<IClient>();
            var configurer = new AutofacConfigurer(builder);
            var container = configurer.GetContainer();
            PainterResolver.DrawTagCloud(container);
        }
    }
}
