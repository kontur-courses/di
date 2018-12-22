using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace TagsCloudVisualization
{
    public class Program
    {
        public static void Main()
        {
            var image = new RectangleLayoutVisualizer()
                .Visualize(new Size(1000, 1000), GenerateRandomLayout(), new Point(0,0));
            new ImageSaver().Save(image, "file.png");
        }

        private static Rectangle[] GenerateRandomLayout()
        {
            var circularCloudLayouter = new CircularCloudLayouter(new Point(0,0));
            var random = new Random(52);

            for (int i = 0; i < 40; i++)
            {
                var size = new Size(random.Next(20, 100), random.Next(20, 100));
                circularCloudLayouter.PutNextRectangle(size);
            }

            return circularCloudLayouter.PastRectangles.ToArray();
        }
    }
}
