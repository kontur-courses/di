using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class Program
    {
        static void Main(string[] args)
        {
            var circularCloudLayouter = new CircularCloudLayouter(new Point(1000, 1000));
            var rectangles = new List<Rectangle>();
            var exampleNumber = 0;
            while (exampleNumber++ < 3)
            {
                var rnd = new Random();
                var count = 20;
                while (count-- > 0)
                    rectangles.Add(circularCloudLayouter.PutNextRectangle(new Size(rnd.Next(10, 200), rnd.Next(10, 200))));

                var exampleImage = RectanglesRenderer.GenerateImage(rectangles);
                exampleImage.Save($"./example_{exampleNumber}.jpg");
            }
        }
    }
}
