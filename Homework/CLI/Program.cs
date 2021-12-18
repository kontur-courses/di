using Autofac;
using ContainerConfigurers;
using TagsCloudContainer;
using TagsCloudContainer.Client;

namespace CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new Client(args).UserConfig;
            var container = new AutofacConfigurer(config).GetContainer();
            using (var scope = container.BeginLifetimeScope())
            {
                var painter = scope.Resolve<CloudPainter>();
                painter.Draw(scope.Resolve<IUserConfig>().OutputFile);
            }
        }
    }
}
