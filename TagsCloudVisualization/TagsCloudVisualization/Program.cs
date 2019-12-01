using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var rnd = new Random();
            var layouter = new CircularCloudLayouter(new Point(500, 500));

            for (var i = 0; i < 100; i++)
                layouter.PutNextRectangle(new Size(10 + rnd.Next(100),
                    10 + rnd.Next(100)));

            TagCloudVisualizer.Visualize(layouter, new Size(1000, 1000))
                .Save("result.png", ImageFormat.Png);

            Process.Start("result.png");
        }
    }
}
