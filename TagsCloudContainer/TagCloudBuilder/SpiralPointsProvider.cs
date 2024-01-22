using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class SpiralPointsProvider : IPointsProvider
    {
        private int pointNumber = 0;
        private readonly float angleStep = (float)Math.PI / 21;
        private readonly float SpiralRadius = 50;
        private readonly Point Center;

        public SpiralPointsProvider(Point center)
        {
            Center = center;
        }

        public IEnumerable<Point> Points()
        {
            while (pointNumber < 10000000) // Limit number of returned points for safety reason
            {
                var r = Math.Sqrt(SpiralRadius * pointNumber);
                var angle = angleStep * pointNumber;
                var x = r * Math.Cos(angle) + Center.X;
                var y = r * Math.Sin(angle) + Center.Y;
                pointNumber++;
                yield return new Point((int)x, (int)y);
            }
            throw new ArgumentException("Reach end of placing points");
        }

        public void Reset()
        {
            pointNumber = 0;
        }
    }
}
