using Autofac;
using TagsCloudVisualization;

namespace TagsCloudCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = new SettingProvider().GetSettings();
            var builder = new ContainerBuilder();
            builder.RegisterModule(new TagsCloudDrawerModule(settings));
            var container = builder.Build();
            container.Resolve<Visualizer>().Visualize();
        }
    }
}