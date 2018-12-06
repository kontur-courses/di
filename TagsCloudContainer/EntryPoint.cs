using System;
using System.Drawing;
using System.IO;
using TagsCloudContainer.Algo.Geom;
using TagsCloudContainer.Drawing;

namespace TagsCloudContainer
{
    public class EntryPoint
    {
        private const int DefaultImageWidth = 1024;
        private const int DefaultImageHeight = 1024;

        private static void Main(string[] args)
        {
            Console.WriteLine(File.Exists(@"Resources\mystem.exe"));

            //var randomLayouter = LayouterWithRandomSizeRectangles();
            //Console.WriteLine("Random generated");
            //var simpleLayouter = SimpleLayouter();
            //Console.WriteLine("Simple generated");

            //var writer = new BMPWriter();
            //var imageDrawer = new ImageDrawer();

            //writer.WriteToFIle(imageDrawer.Draw(randomLayouter), "random.png");
            //writer.WriteToFIle(imageDrawer.Draw(simpleLayouter), "simple.png");
        }

        private static CircularCloudLayout LayouterWithRandomSizeRectangles()
        {
            var random = new Random();
            var layouter = new CircularCloudLayout(DefaultImageWidth / 2, DefaultImageHeight / 2, DefaultImageWidth, DefaultImageHeight);
            for (var i = 0; i < 800; i++)
            {
                var randomSize = new Size(random.Next(3, 50), random.Next(3, 50));
                layouter.PutNextRectangle(randomSize);
            }

            return layouter;
        }

        private static CircularCloudLayout SimpleLayouter()
        {
            var layouter = new CircularCloudLayout(DefaultImageWidth / 2, DefaultImageHeight / 2, DefaultImageWidth, DefaultImageHeight);
            for (var i = 0; i < 1000; i++)
                layouter.PutNextRectangle(new Size(20, 10));

            return layouter;
        }
    }
}