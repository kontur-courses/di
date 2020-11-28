using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WordCloudGenerator.CircularLayouter
{
    public class CircularLayouter : ILayouter
    {
        private readonly List<RectangleF> rectangles = new List<RectangleF>();
        private readonly Spiral spiral;

        public CircularLayouter(Point center)
        {
            spiral = new Spiral(center);
        }

        public RectangleF PutNextRectangle(SizeF rectangleSize)
        {
            if (rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
                throw new ArgumentException(
                    $"{(rectangleSize.Height <= 0 ? "Height" : "Width")} cant be negative or zero");

            var rectangleToAdd = new RectangleF {Size = rectangleSize};

            do
            {
                rectangleToAdd = rectangleToAdd.SetCenter(spiral.GetNextPoint());
            } while (rectangles.Any(rectangle => rectangle.IntersectsWith(rectangleToAdd)));

            rectangles.Add(rectangleToAdd);
            return rectangleToAdd;
        }

        public RectangleF[] GetRectangles()
        {
            return rectangles.ToArray();
        }
    }
}