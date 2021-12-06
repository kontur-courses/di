using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.CloudLayouter
{
    public class ArchimedeanSpiral : ISpiral
    {
        private readonly Point center;
        private readonly double angleStep;
        private readonly HashSet<Point> points;
        private double angle;

        public ArchimedeanSpiral(Point center, double angleStep = Math.PI / 90)
        {
            this.center = center;
            this.angleStep = angleStep;
            points = new HashSet<Point>();
        }

        public Point GetNextPoint()
        {
            var nextPoint = CalculateNextPoint();
            while (points.Contains(nextPoint))
            {
                nextPoint = CalculateNextPoint();
            }

            points.Add(nextPoint);
            return nextPoint;
        }

        private Point CalculateNextPoint()
        {
            var x = (int)Math.Round(angle * Math.Cos(angle));
            var y = (int)Math.Round(angle * Math.Sin(angle));

            angle += angleStep;
            return new Point(center.X + x, center.Y + y);
        }
    }
}