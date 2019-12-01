using System.Diagnostics;
using System.Drawing.Imaging;
using Autofac;

namespace TagsCloudVisualization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TagCloudVisualizer>().As<TagCloudVisualizer>();
            builder.Register(context => ImageSettings.InitializeDefaultSettings()).As<ImageSettings>();
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
            builder.RegisterType<TextParser>().As<IParser>();
            var container = builder.Build();

            var image = container.Resolve<TagCloudVisualizer>().VisualizeTextFromFile("InputData/Input1.txt");

            image.Save("result.png", ImageFormat.Png);
            Process.Start("result.png");
        }
    }
}