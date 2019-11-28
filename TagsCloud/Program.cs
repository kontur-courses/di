using System;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var maxSize = new Size(80, 40);
            var minSize = new Size(40, 20);
            var rectangles = RectanglesGenerator.GenerateRectangles(400, maxSize, minSize);
            var image = CloudVisualizer.Visualize(rectangles.ToArray());
            image.Save("../../layout4.png");
        }
    }
}