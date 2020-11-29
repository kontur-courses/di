using System.Drawing;
using Autofac;

namespace TagCloud
{
    public class Program
    {
        public static void Main()
        {
            var builder = new ContainerBuilder();
            var center = new Point(0, 0);
            // builder.RegisterInstance(center).As<Point>();
            const double density = 0.05;
            const int angelStep = 5;
            builder.RegisterType<Spiral>().As<ICurve>()
                .WithParameter("center", center)
                .WithParameter("density", density)
                .WithParameter("angelStep", angelStep);
            builder.RegisterType<CircularCloudLayouter>().As<ITagCloud>();
            builder.RegisterType<TagCloudVisualizer>().As<IVisualizer>();

            var container = builder.Build();

            var bitmap = container.Resolve<IVisualizer>().CreateBitMap(1920, 1080);
            bitmap.Save("test.png");

            // var tagCloud = new CircularCloudLayouter(new Point(1920 / 2, 1080 / 2));
            // var random = new Random();
            // for (var i = 0; i < 100; i++)
            // tagCloud.PutNextRectangle(new Size(random.Next() % 100 + 1, random.Next() % 100 + 1));
            // var vis = new TagCloudVisualizer(tagCloud);
            // var image = vis.CreateBitMap(1920, 1080);
            // image.Save($"100RectanglesDensity1.jpg");
        }
    }
}