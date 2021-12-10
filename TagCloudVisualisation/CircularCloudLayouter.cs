using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloudVisualisation
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly ISpiral spiral;
        private readonly List<Rectangle> rectangles = new();

        public CircularCloudLayouter(ISpiral spiral)
        {
            this.spiral = spiral;
        }

        public List<Rectangle> GetRectangles()
        {
            return rectangles;
        }

        public Rectangle PutNewRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
            {
                throw new ArgumentException("Size must be positive");
            }

            var rectangle = CreateRectangleFromSpiral(rectangleSize);
            while (rectangle.IntersectsWith(rectangles))
            {
                rectangle = CreateRectangleFromSpiral(rectangleSize);
            }

            rectangles.Add(rectangle);
            return rectangle;
        }

        private Rectangle CreateRectangleFromSpiral(Size rectangleSize)
        {
            var currentPoint = spiral.GetNextPoint();
            return currentPoint.GetRectangleWithCenterInPoint(rectangleSize);
        }
    }
}
