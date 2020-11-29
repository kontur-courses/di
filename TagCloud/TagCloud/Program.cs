using System;
using System.Drawing;

namespace TagCloud
{
    public class Program
    {
        public static void Main()
        {
            var tagCloud = new CircularCloudLayouter(new Point(1920 / 2, 1080 / 2));
            var random = new Random();
            for (var i = 0; i < 100; i++)
                tagCloud.PutNextRectangle(new Size(random.Next() % 100 + 1, random.Next() % 100 + 1));
            var vis = new TagCloudVisualizer(tagCloud);
            var image = vis.CreateBitMap(1920, 1080);
            image.Save($"100RectanglesDensity1.jpg");
        }
    }
}