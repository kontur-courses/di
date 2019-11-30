using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public static class Program
    {
        public static void Main()
        {
            var layouter = new CircularCloudLayouter(new Point(250, 250));
            var rectangles = new List<Rectangle>();
            for (var i = 0; i < 200; i++)
                rectangles.Add(layouter.PutNextRectangle(new Size(15, 15)));
            var painter = new Painter(new Size(500, 500));
            var image = painter.GetSingleColorCloud(Color.Coral, rectangles);
            ImageSaver.SaveImageToDefaultDirectory("example", image);
        }
    }
}