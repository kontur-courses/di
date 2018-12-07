using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly List<Rectangle> allRectangles;
        private readonly CircularSpiral spiral;

        public CircularCloudLayouter(Point center)
        {
            Center = center;
            allRectangles = new List<Rectangle>();
            spiral = new CircularSpiral(center, 0.1, 0.01);
        }

        public Point Center { get; }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (SizeHaveZeroOrNegativeValue(rectangleSize))
                throw new ArgumentException("Size cannot be zero or negative");
            var rectangle = GetNextRectangle(rectangleSize);
            allRectangles.Add(rectangle);
            return rectangle;
        }

        private Rectangle GetNextRectangle(Size rectangleSize)
        {
            while (true)
            {
                var newCoordinate = spiral.GetNextCoordinate();
                var rectangle = CreateCenteredRectangle(rectangleSize, newCoordinate);
                if (!IsIntersectWithExistingRectangles(rectangle))
                    return rectangle;
            }
        }

        private Rectangle CreateCenteredRectangle(Size rectangleSize, Point pointOnSpiral)
        {
            var x = pointOnSpiral.X - rectangleSize.Width / 2;
            var y = pointOnSpiral.Y - rectangleSize.Height / 2;
            return new Rectangle(x, y, rectangleSize.Width, rectangleSize.Height);
        }

        private bool IsIntersectWithExistingRectangles(Rectangle rectangle)
        {
            foreach (var existingRectangle in allRectangles)
                if (rectangle.IntersectsWith(existingRectangle))
                    return true;
            return false;
        }

        private bool SizeHaveZeroOrNegativeValue(Size size)
        {
            return size.Width <= 0 || size.Height <= 0;
        }
    }
}