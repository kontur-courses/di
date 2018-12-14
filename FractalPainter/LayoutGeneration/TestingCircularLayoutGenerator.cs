using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.LayoutGeneration
{
    public class TestingCircularLayoutGenerator
    {
        private ICloudLayouter layouter;

        public TestingCircularLayoutGenerator(Point center)
        {
            layouter = new CircularCloudLayouter(center);
        }

        public List<Rectangle> GenerateTestLayout()
        {
            var rectangles = new List<Rectangle>();
            var rectangleCount = 20;
            var x = 90;
            var y = 20;

            var random = new Random();
            for (var i = 1; i < rectangleCount; i++)
            {
                if (i % 50 == 0)
                    x -= random.Next(0,5);
                var size = new Size(x, y);
                rectangles.Add(layouter.PutNextRectangle(size));
            }

            return rectangles;
        }
    }
}