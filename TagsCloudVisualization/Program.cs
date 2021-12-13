using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Autofac;
using TagsCloudVisualization.Default;

namespace TagsCloudVisualization
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TxtFileReader>().As<IFileReader>();
            builder.RegisterType<RandomTagColor>().As<ITokenColorChooser>();
            builder.RegisterType<TokenSortedOrder>().As<ITokenOrderer>();
            builder.RegisterType<WordCounter>().As<ITokenWeigher>();
            builder.RegisterType<WordSelector>().UsingConstructor().As<IWordSelector>();
            builder.RegisterInstance(new CircularCloudMaker(Point.Empty)).As<ICloudMaker>();
            builder.RegisterType<FileReader>();
            builder.RegisterType<TokenGenerator>();
            builder.RegisterType<TagCloudMaker>();
            builder.RegisterType<TagCloudVisualiser>();
            builder.RegisterType<TagCloud>();
            var container = builder.Build();
            var scope = container.BeginLifetimeScope();
            var tagCloud = scope.Resolve<TagCloud>();
            var file = new FileInfo("Example.txt");
            var font = new Font("Comic Sans MS", 9);
            var resolution = new Size(1280, 720);
            tagCloud.CreateTagCloudFromFile(file, font, 100, resolution, "testimage.png", ImageFormat.Png);
            Process.Start(new ProcessStartInfo("testimage.png")
            { UseShellExecute = true });
        }
        
    }
}