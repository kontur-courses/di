using System.Drawing;
using TagCloud.Layouter;
using TagCloud.PointGenerator;

namespace TagCloud;

    public class Program
    {
        private const int Width = 1920;
        private const int Height = 1080;

        static void Main(string[] args)
        {
            var layouter = new CircularCloudLayouter(new SpiralGenerator(new Point(Width / 2, Height / 2), 1, 0.01));

            var random = new Random();

            for (var i = 0; i < 150; i++)
            {
                layouter.PutNextRectangle(new Size(50 + random.Next(0, 100), 50 + random.Next(0, 100)));
            }

            var saver = new CloudSaver.CloudSaver();

            using var bitmap = CloudDrawer.CloudDrawer.DrawTagCloud(layouter.Rectangles);
            saver.Save(bitmap, "Sample", "png");
        }
    }