using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    /// <summary>
    /// Спираль. Выдает последовательность точек используя настройки заданные пользователем (плотность).
    /// </summary>
    public class Spiral
    {
        private readonly double angleOffset;
        private readonly double radiusOffset;
        private readonly Point center;
        private readonly List<Point> usedPoints;
        private double angle;
        private double radius;

        public Spiral(CustomSettings settings)
        {
            center = new Point(settings.CanvasWidth / 2, settings.CanvasHeight / 2);
            usedPoints = new List<Point>();
            angleOffset = settings.AngleOffset;
            radiusOffset = settings.RadiusOffset;
        }

        public IEnumerable<Point> GetPoints()
        {
            foreach (var point in usedPoints)
                yield return point;
            while (true)
            {
                radius += radiusOffset;
                angle += angleOffset;
                var x = center.X + (int) Math.Round(radius * Math.Cos(angle));
                var y = center.Y + (int) Math.Round(radius * Math.Sin(angle));
                usedPoints.Add(new Point(x, y));
                yield return new Point(x, y);
            }
        }
    }
}