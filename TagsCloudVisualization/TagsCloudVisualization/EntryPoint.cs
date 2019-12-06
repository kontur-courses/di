using System.Diagnostics;
using Autofac;

namespace TagsCloudVisualization
{
    public class EntryPoint
    {
        public static void Main(string[] args)
        {
            var container = InitializeContainer();
            var imageSettings = ImageSettings.DefaultSettings;
            var visualizer = container.Resolve<IVisualizer>();
            visualizer.VisualizeTextFromFile("InputData/Input2.txt", imageSettings).Save("result.png");
            Process.Start("result.png");
        }

        public static IContainer InitializeContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TagCloudVisualizer>().SingleInstance().As<IVisualizer>();
            builder.RegisterType<CircularCloudLayouter>().SingleInstance().As<ILayouter>();
            builder.RegisterType<ArchimedeanSpiral>().SingleInstance().As<ICirclePointLocator>();
            builder.RegisterType<TextParser>().SingleInstance().As<IParser>();
            builder.RegisterType<RandomTagPainter>().SingleInstance().As<ITagPainter>();
            return builder.Build();
        }
    }
}