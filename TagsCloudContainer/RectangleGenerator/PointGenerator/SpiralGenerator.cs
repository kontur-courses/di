using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.RectangleGenerator.PointGenerator
{
    public class SpiralGenerator : IPointGenerator
    {
        public Point Center { get; }
        private IEnumerator<Point> Generator { get; }
        private readonly HashSet<Point> prevPoints = new HashSet<Point>();

        public SpiralGenerator(Point center)
        {
            Center = center;
            Generator = GetNextPoints().GetEnumerator();
        }

        public Point GetNextPoint()
        {
            Generator.MoveNext();
            return Generator.Current;
        }

        private IEnumerable<Point> GetNextPoints()
        {
            float angle = 0;
            const float offsetAngle = (float)(5 * Math.PI / 180);
            while (true)
            {
                var radius = 2 * angle;
                var point = new Point(
                    (int)(Center.X + radius * Math.Cos(angle)),
                    (int)(Center.Y + radius * Math.Sin(angle)));
                if (!prevPoints.Contains(point))
                {
                    prevPoints.Add(point);
                    yield return point;
                }
                angle += offsetAngle;
            }
            // ReSharper disable once IteratorNeverReturns
        }
    }
}
