using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        private readonly Vector cloudCenter;
        private readonly List<Rectangle> rectangles;

        public IReadOnlyList<Rectangle> Rectangles => rectangles.AsReadOnly();
        private readonly Random random = new Random();
        private double radius = 10;
        private const double AngleShift = Math.PI / 18;
        private const int RadiusShift = 10;

        public CircularCloudLayouter(Point center)
        {
            if (center.X < 0 || center.Y < 0)
                throw new ArgumentException($"Coordinates must be non-negative, but was ({center.X},{center.Y})");

            cloudCenter = new Vector(center);
            rectangles = new List<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
                throw new ArgumentException(
                    $"Size should be Positive, but was ({rectangleSize.Height}, {rectangleSize.Width})");

            var sizeVector = new Vector(rectangleSize);
            Rectangle newRectagle;
            if (rectangles.Count == 0)
            {
                radius = Math.Min(rectangleSize.Height, rectangleSize.Width) / 2.0;
                var leftTop = cloudCenter - sizeVector / 2;
                newRectagle = new Rectangle(leftTop, sizeVector);
            }
            else
                newRectagle = FindPositionOfNewRectangle(sizeVector);
            rectangles.Add(newRectagle);
            return newRectagle;
        }

        private Rectangle FindPositionOfNewRectangle(Vector sizeVector)
        {
            var curentRadius = radius;
            while (true)
            {
                var startAngle = random.NextDouble() * 2 * Math.PI;
                for (var angle = startAngle; angle < startAngle + 2 * Math.PI; angle += AngleShift)
                {
                    var newRectangleCenter = cloudCenter + curentRadius * Vector.Angle(angle);
                    var leftTop = newRectangleCenter - sizeVector / 2;
                    var candidate = new Rectangle(leftTop, sizeVector);
                    if (!candidate.IntersectWithAny(rectangles))
                        return candidate;
                }
                curentRadius += RadiusShift;
            }
        }
    }


}