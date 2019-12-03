using System.Diagnostics;
using System.Drawing.Imaging;
using Autofac;

namespace TagsCloudVisualization
{
    public class EntryPoint
    {
        public static void Main(string[] args)
        {
            var container = InitializeContainer();

            container
                .Resolve<IVisualizer>()
                .VisualizeTextFromFile("InputData/Input1.txt")
                .TrySaveTo("result.jpg", ImageFormat.Jpeg);

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