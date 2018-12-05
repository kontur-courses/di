using System;
using System.Collections.Generic;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization.Layouter
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private const double SpiralAngleInterval = 0.1;
        private const double SpiralTurnsInterval = 0.5;
        private double currentSpiralAngle;

        private readonly Point origin;
        private readonly List<Rectangle> rectanglesList;

        public CircularCloudLayouter(Point origin)
        {
            this.origin = origin;
            rectanglesList = new List<Rectangle>();
        }

        public IReadOnlyCollection<Rectangle> GetCloud()
        {
            return rectanglesList;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var rectangle = PutOnSpiral(rectangleSize);
            if (rectangle == null)
                return null;
            rectangle = MakeCloserToCenter(rectangle);
            rectanglesList.Add(rectangle);
            return rectangle;
        }

        private Rectangle MakeCloserToCenter(Rectangle rectangle)
        {
            var directionToCenter = new Vector(rectangle.Center, origin).Normalized();
            var currentDirection = directionToCenter;
            var previousPosition = new Point(0, 0);
            while (directionToCenter.IsSameDirection(currentDirection)
                   && !rectangle.IsIntersectsWithAnyRect(rectanglesList))
            {
                previousPosition = rectangle.Center;
                rectangle.Center += directionToCenter;
                currentDirection = new Vector(rectangle.Center, origin).Normalized();
            }

            rectangle.Center = previousPosition;
            return rectangle;
        }

        private Rectangle PutOnSpiral(Size rectangleSize)
        {
            var newRectangle = new Rectangle(origin, rectangleSize);
            while (newRectangle.IsIntersectsWithAnyRect(rectanglesList))
            {
                currentSpiralAngle += SpiralAngleInterval;
                var rectCenter = ArithmeticSpiral(currentSpiralAngle, SpiralTurnsInterval);
                newRectangle.Center = rectCenter;
            }

            return newRectangle;
        }

        private Point ArithmeticSpiral(double angle, double turnsInterval)
        {
            var x = origin.X + turnsInterval * angle * Math.Cos(angle);
            var y = origin.Y + turnsInterval * angle * Math.Sin(angle);

            return new Point(x, y);
        }
    }
}