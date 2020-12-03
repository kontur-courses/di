using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Autofac;
using TagCloud.Curves;
using TagCloud.WordsFilter;
using TagCloud.WordsProvider;

namespace TagCloud
{
    public class Program
    {
        public static void Main()
        {
            var builder = new ContainerBuilder();
            const int width = 1920;
            const int height = 1080;
            var center = new Point(width / 2, height / 2);
            const double density = 0.05;
            const int angelStep = 5;
            var colors = new[]
                {Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Aqua, Color.Blue, Color.Purple};
            var fontFamily = "Times New Roman";
            builder.RegisterType<ArchimedeanSpiral>().As<ICurve>()
                .WithParameter("center", center)
                .WithParameter("density", density)
                .WithParameter("angelStep", angelStep);
            builder.RegisterType<CircularCloudLayouter>().As<ITagCloud>().SingleInstance();
            builder.RegisterType<TagCloudVisualizer>().As<IVisualizer>();
            var wordsFilePath = Path.GetFullPath("../../../../words.doc");
            builder.RegisterType<MicrosoftWordWordsProvider>().As<IWordsProvider>()
                .WithParameter("filePath", wordsFilePath);
            var wordsFilter = new WordsFilter.WordsFilter().Normalize().RemovePrepositions();
            builder.RegisterInstance(wordsFilter).As<IWordsFilter>();

            builder.RegisterInstance(colors).As<Color[]>();
            var container = builder.Build();

            container.Resolve<ITagCloud>().GenerateTagCloud();
            var bitmap = container.Resolve<IVisualizer>()
                .CreateBitMap(width, height, container.Resolve<Color[]>(), fontFamily);
            bitmap.Save("../../../../Examples/test.png", ImageFormat.Png);
        }
    }
}