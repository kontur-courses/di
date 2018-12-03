using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.RectanglesLayouter.PointsGenerator
{
    public class SpiralPointsGenerator : IPointsGenerator
    {
        private readonly int distanceBetweenPoints;
        private readonly double angleIncrement;

        public SpiralPointsGenerator(int distanceBetweenPoints, double angleIncrement)
        {
            if (distanceBetweenPoints <= 0 || angleIncrement <= 0)
                throw new ArgumentException("Distance between points and angle increment should be positive numbers");
            this.distanceBetweenPoints = distanceBetweenPoints;
            this.angleIncrement = angleIncrement;
        }

        public IEnumerable<Point> GetPoints()
        {
            double currentSpiralAngle = 0;
            while (true)
            {
                var radius = distanceBetweenPoints * currentSpiralAngle;
                var x = radius * Math.Cos(currentSpiralAngle);
                var y = radius * Math.Sin(currentSpiralAngle);
                currentSpiralAngle += angleIncrement;
                var point = new Point((int) Math.Round(x), (int) Math.Round(y));
                yield return point;
            }
        }
    }
}