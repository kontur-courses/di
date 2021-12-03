using System;
using System.Drawing;

namespace TagsCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var center = new Point(500, 500);
            var layouter = new CircularCloudLayouter(center);
            var minSize = 40;
            var maxSize = 100;
            var rectanglesCount = 150;
            layouter.PutManyRectangles(rectanglesCount, new Random(),
                minSize, maxSize);

            var imageSize = new Size(1000, 1000);
            CloudImageGenerator.CreateImage(layouter.Cloud, imageSize);
        }
    }
}
