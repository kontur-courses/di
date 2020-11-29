using System;
using System.Drawing;

namespace TagCloud.Infrastructure.Layout.Strategies
{
    public class SpiralStrategy
    {
        private readonly Point center;
        private readonly int angleIncrement;
        public SpiralStrategy(Point center, int angleIncrement)
        {
            this.center = center;
            this.angleIncrement = angleIncrement;
        }

        public Point GetPoint(Func<Point, bool> isValidPoint)
        {
            var angle = 0;
            
            var obtainedPoint = Point.Empty;
            while (!isValidPoint(obtainedPoint))
            {
                var possiblePoint = center + new Size((int) (Math.Sin(angle) * angle), (int) (Math.Cos(angle) * angle));
                obtainedPoint = possiblePoint;
                angle += angleIncrement;
            }
            return OptimizePoint(obtainedPoint, isValidPoint);;
        }

        private Point OptimizePoint(Point obtainedPoint, Func<Point, bool> isValidPoint)
        {
            var possiblePosition = obtainedPoint;
            while (isValidPoint(possiblePosition))
            {
                obtainedPoint = possiblePosition;
                var optimizationOffset = new Size(
                    Math.Sign(center.X - obtainedPoint.X),
                    Math.Sign(center.Y - obtainedPoint.Y));
                if (optimizationOffset.IsEmpty)
                    return obtainedPoint;
                possiblePosition = obtainedPoint + optimizationOffset;
            }
            return obtainedPoint;
        }
    }
}