using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloudVisualization
{
    public class CircularCloudLayouter
    {
        private readonly List<Rectangle> rectangles = new List<Rectangle>();

        private readonly AbstractSpiralGenerator spiralGenerator;

        public CircularCloudLayouter(Point center, AbstractSpiralGenerator generator)
        {
            Center = center;
            spiralGenerator = generator.Begin(center);
        }

        public Point Center { get; }

        public IEnumerable<Rectangle> Rectangles => rectangles;

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException("size has non positive parts");

            var nextPosition = spiralGenerator.Next();
            var rectangle = new Rectangle(nextPosition, rectangleSize);
            while (DoesIntersectWithPreviousRectangles(rectangle))
            {
                nextPosition = spiralGenerator.Next();
                rectangle = new Rectangle(nextPosition, rectangleSize);
            }

            rectangle = AdjustPosition(rectangle);
            rectangles.Add(rectangle);
            return rectangle;
        }

        private Rectangle AdjustPosition(Rectangle rectangle)
        {
            var oldRectangle = rectangle;
            var centerDirection = Center - rectangle.Location;
            var shiftX = Point.UnaryX * Math.Sign(centerDirection.X);
            var shiftY = Point.UnaryY * Math.Sign(centerDirection.Y);
            var stepsAmount = 100;
            while (stepsAmount > 0)
            {
                rectangle.Location += shiftX;
                if (DoesIntersectWithPreviousRectangles(rectangle))
                    rectangle.Location -= shiftX;

                rectangle.Location += shiftY;
                if (DoesIntersectWithPreviousRectangles(rectangle))
                    rectangle.Location += shiftY;

                stepsAmount--;
            }

            return DoesIntersectWithPreviousRectangles(rectangle) ? oldRectangle : rectangle;
        }

        private bool DoesIntersectWithPreviousRectangles(Rectangle rectangle) =>
            rectangles.Any(rectangle.IntersectsWith);

        public IEnumerable<Rectangle> PutNextRectangles(IEnumerable<Size> rectanglesSizes) =>
            rectanglesSizes.Select(PutNextRectangle);
    }
}
