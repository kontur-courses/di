using Autofac;
using TagsCloudContainer.Client;

namespace TagsCloudContainer
{
    public static class PainterResolver
    {
        public static void DrawTagCloud(IContainer container)
        {
            using (var scope = container.BeginLifetimeScope())
            {
                var painter = scope.Resolve<CloudPainter>();
                painter.Draw(scope.Resolve<UserConfig>().OutputFile);
            }
        }
    }
}
