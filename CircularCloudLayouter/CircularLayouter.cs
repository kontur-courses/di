using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CircularCloudLayouter
{
    public class CircularLayouter
    {
        private readonly Spiral spiral;
        private readonly List<Rectangle> rectangles = new List<Rectangle>();

        public CircularLayouter(Point center)
        {
            spiral = new Spiral(center);
        }

       public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
                throw new ArgumentException($"{(rectangleSize.Height <= 0 ? "Height" : "Width")} cant be negative or zero");

            var rectangleToAdd = new Rectangle {Size = rectangleSize};

            do
                rectangleToAdd = rectangleToAdd.SetCenter(spiral.GetNextPoint());
            while (rectangles.Any(rectangle => rectangle.IntersectsWith(rectangleToAdd)));

            rectangles.Add(rectangleToAdd);
            return rectangleToAdd;
        }

        public Rectangle[] GetRectangles()
        {
            return rectangles.ToArray();
        }
    }
}