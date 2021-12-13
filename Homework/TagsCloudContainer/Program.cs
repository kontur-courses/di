using Autofac;
using TagsCloudContainer.ContainerConfigurers;
using TagsCloudContainer.Clients;

namespace TagsCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            var ac = new AutofacConfigurer(args, builder);
            var container = ac.GetContainer();
            DrawTagCloud(container);
        }

        private static void DrawTagCloud(IContainer container)
        {
            using (var scope = container.BeginLifetimeScope())
            {
                var painter = scope.Resolve<CloudPainter>();
                painter.Draw(scope.Resolve<UserConfig>().OutputFile);
            }
        }
    }
}
