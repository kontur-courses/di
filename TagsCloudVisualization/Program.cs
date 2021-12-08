using System.Drawing;
using Autofac;

namespace TagsCloudVisualization
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = new Settings
            {
                Directory = "ImageExamples",
                ImageName = "example.bmp",
                FontFamilyName = "Arial",
                TagColor = Color.Firebrick,
                StartPoint = new Point(0, 0),
                FileWithWords = "text.txt",
                MaxFontSize = 100,
                BoringWords = new[] { "в", "что", "не", "и", "с", "на", "то", "а", "он", "его", "для", "из" }
            };
            var builder = new ContainerBuilder();
            builder.RegisterModule(new TagsCloudDrawerModule(settings));
            var container = builder.Build();
            container.Resolve<Visualizer>().Visualize();
        }
    }
}