using System.Drawing;
using System.Drawing.Imaging;
using Autofac;

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
            builder.RegisterType<Spiral>().As<ICurve>()
                .WithParameter("center", center)
                .WithParameter("density", density)
                .WithParameter("angelStep", angelStep);
            builder.RegisterType<CircularCloudLayouter>().As<ITagCloud>().SingleInstance();
            builder.RegisterType<TagCloudVisualizer>().As<IVisualizer>();
            builder.RegisterType<TxtWordsProvider>().As<IWordsProvider>()
                .WithParameter("filePath", "../../../../words.txt");
            var wordsFilter = new WordsFilter().Normalize().RemovePrepositions();
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