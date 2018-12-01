using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public static class CircularCloudLayouterGenerator
    {
        public static List<Rectangle> GenerateRectanglesSet(CircularCloudLayouter layouter, int count,
            int widthBottomBound, int widthTopBound, int heightBottomBound, int heightTopBound)
        {
            var rectangles = new List<Rectangle>();
            var random = new Random();

            for (var i = 0; i < count; i++)
            {
                var randomSize = new Size(random.Next(widthBottomBound, widthTopBound),
                    random.Next(heightBottomBound, heightTopBound));
                var newRectangle = layouter.PutNextRectangle(randomSize);
                rectangles.Add(newRectangle);
            }

            return rectangles;
        }
    }
}
