using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.CloudLayouter
{
    public class ArchimedeanSpiral : ISpiral
    {
        private readonly Point center;
        private readonly double angleStep;
        private readonly double density;
        private readonly HashSet<Point> points;
        private double angle;
        private double Radius => angle * density;

        public ArchimedeanSpiral(IAppSettings appSettings)
        {
            center = Point.Empty;
            angleStep = appSettings.AngleStep;
            density = appSettings.Density;
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
            var x = (int)Math.Round(Radius * Math.Cos(angle));
            var y = (int)Math.Round(Radius * Math.Sin(angle));

            angle += angleStep;
            return new Point(center.X + x, center.Y + y);
        }
    }
}