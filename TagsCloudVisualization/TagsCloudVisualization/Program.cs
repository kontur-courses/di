using System.Diagnostics;
using System.Drawing.Imaging;
using Autofac;

namespace TagsCloudVisualization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var container = InitializeContainer();

            var image = container.Resolve<IVisualizer>().VisualizeTextFromFile("InputData/Input1.txt");

            image.Save("result.png", ImageFormat.Png);
            Process.Start("result.png");
        }

        public static IContainer InitializeContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TagCloudVisualizer>().SingleInstance().As<IVisualizer>();
            builder.Register(context => ImageSettings.InitializeDefaultSettings()).SingleInstance().As<ImageSettings>();
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
            builder.RegisterType<ArchimedeanSpiral>().As<ICirclePointLocator>();
            builder.RegisterType<TextParser>().SingleInstance().As<IParser>();
            return builder.Build();
        }
    }
}