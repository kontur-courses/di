using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Autofac;
using TagsCloudVisualization;

namespace TagCloud
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var pictureSize = new Size(2000, 2000);
            var fontName = "Arial";
            var fontColor = Color.Black;
            var maxFontSize = 40;
            var inputFile = "1.txt";
            var outputFile = "qwe.bmp";
            
            var builder = new ContainerBuilder();
            builder.RegisterType<CloudDrawer>().As<ICloudDrawer>();
            builder.RegisterType<WordsNormalizer>().As<IWordsNormalizer>();
            builder.RegisterType<CircularCloudLayouter>().As<ITagCloudLayouter>();
            builder.RegisterType<SpiralPoints>().As<IPoints>()
                   .WithParameter("center", new Point(pictureSize.Height / 2, pictureSize.Width / 2));

            builder.RegisterType<WordsForCloudGenerator>().As<IWordsForCloudGenerator>()
                   .WithParameters(new[]
                   {
                       new NamedParameter("fontName", fontName),
                       new NamedParameter("color", fontColor),
                       new NamedParameter("maxFontSize", maxFontSize)
                   });

            builder.RegisterType<CloudDrawer>().As<ICloudDrawer>().WithParameter("pictureSize", pictureSize);

            builder.RegisterType<FileReader>().As<IFileReader>().WithParameter("path", inputFile);
            builder.RegisterType<TagCloudCreator>().As<ITagCloudCreator>();

            var bulded = builder.Build().Resolve<ITagCloudCreator>();
            var bitmap = bulded.GetCloud();
            bitmap.Save(outputFile);
        }
    }
}