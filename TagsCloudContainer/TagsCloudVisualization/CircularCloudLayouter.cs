using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly List<Rectangle> rectangles = new();
        private readonly Point center;
        private readonly ArchimedeanSpiralPath spiralPath;

        public CircularCloudLayouter(Point center = default) : this(
            center,
            new ArchimedeanSpiralPath(new ArchimedeanSpiral(radius: 0.1))) { }

        public CircularCloudLayouter(Point center, ArchimedeanSpiralPath spiralPath)
        {
            this.center = center;
            this.spiralPath =
                spiralPath ?? throw new ArgumentException("Spiral path refers to null.", nameof(spiralPath));
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException("Size must be positive.", nameof(rectangleSize));

            var rectangle = CreateAtCenter(rectangleSize);
            var suitableLocation = FindLocation(rectangle);
            var suitableRectangle = new Rectangle(suitableLocation, rectangleSize);
            rectangles.Add(suitableRectangle);
            return suitableRectangle;
        }

        private Point FindLocation(Rectangle rectangle)
        {
            var initialLocation = rectangle.Location;
            while (true)
            {
                var offset = spiralPath.GetNextPoint();

                rectangle.X = initialLocation.X - offset.X;
                rectangle.Y = initialLocation.Y - offset.Y;
                if (!IsIntersectAny(rectangle))
                    return rectangle.Location;
            }
        }

        private Rectangle CreateAtCenter(Size rectangleSize) =>
            new Rectangle(
                new Point(-rectangleSize.Width / 2 + center.X, -rectangleSize.Height / 2 + center.Y),
                rectangleSize);

        private bool IsIntersectAny(Rectangle rectangle) =>
            rectangles.Any(other => other.IntersectsWith(rectangle));
    }
}