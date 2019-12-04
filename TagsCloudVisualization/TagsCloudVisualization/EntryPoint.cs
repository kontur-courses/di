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

            container
                .Resolve<IVisualizer>()
                .VisualizeTextFromFile("InputData/Input3.txt", imageSettings)
                .Save("result.png");

            Process.Start("result.png");
        }

        public static IContainer InitializeContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TagCloudVisualizer>().SingleInstance().As<IVisualizer>();
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
            builder.RegisterType<ArchimedeanSpiral>().As<ICirclePointLocator>();
            builder.RegisterType<TextParser>().SingleInstance().As<IParser>();
            builder.RegisterType<RandomTagPainter>().SingleInstance().As<ITagPainter>();
            return builder.Build();
        }
    }
}