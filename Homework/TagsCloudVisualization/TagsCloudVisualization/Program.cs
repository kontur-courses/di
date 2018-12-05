using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    class Program
    {
        static void Main(string[] args)
        {
            var layouter = new CircularCloudLayouter(new Point(500, 500));
            var rnd = new Random();
            for (var i = 0; i < 100; i++)
                layouter.PutNextRectangle(new Size(rnd.Next(5, 100), rnd.Next(5, 100)), new Point(0, 0));
            layouter.ReplaceRectangles();

            Drawer.DrawAndSaveRectangles(new Size(1000, 1000), layouter.Rectangles, "sample.png", @"LastRunSample\");
        }
    }
}
