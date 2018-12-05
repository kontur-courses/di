using System;
using System.Drawing;
using TagsCloudContainer.Drawing;
using TagsCloudContainer.Geom;

namespace TagsCloudContainer
{
    public class EntryPoint
    {
        private const int DefaultImageWidth = 1024;
        private const int DefaultImageHeight = 1024;

        private static void Main(string[] args)
        {
            var randomLayouter = LayouterWithRandomSizeRectangles();
            Console.WriteLine("Random generated");
            var simpleLayouter = SimpleLayouter();
            Console.WriteLine("Simple generated");

            var imageWriter = new ImageWriter();
            var imageDrawer = new ImageDrawer();

            imageWriter.Write(imageDrawer.Draw(randomLayouter), "random.png");
            imageWriter.Write(imageDrawer.Draw(simpleLayouter), "simple.png");
        }

        private static CircularCloudLayouter LayouterWithRandomSizeRectangles()
        {
            var random = new Random();
            var layouter = new CircularCloudLayouter(DefaultImageWidth / 2, DefaultImageHeight / 2, DefaultImageWidth, DefaultImageHeight);
            for (var i = 0; i < 800; i++)
            {
                var randomSize = new Size(random.Next(3, 50), random.Next(3, 50));
                layouter.PutNextRectangle(randomSize);
            }

            return layouter;
        }

        private static CircularCloudLayouter SimpleLayouter()
        {
            var layouter = new CircularCloudLayouter(DefaultImageWidth / 2, DefaultImageHeight / 2, DefaultImageWidth, DefaultImageHeight);
            for (var i = 0; i < 1000; i++)
                layouter.PutNextRectangle(new Size(20, 10));

            return layouter;
        }
    }
}