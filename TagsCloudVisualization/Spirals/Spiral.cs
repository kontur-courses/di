using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Spirals
{
    public class Spiral : ISpiral
    {
        public Point Center { get; }
        private readonly double angleStep;
        public List<Point> Points { get; }
        private double angle;

        public Spiral(Point center, double angleStep = 0.3)
        {
            if (angleStep == 0)
                throw new ArgumentException("Angle step should not be zero");

            Center = center;
            this.angleStep = angleStep;
            Points = new List<Point> { center };
            angle = 0.0;
        }

        public List<Point> GetPoints(int count)
        {
            if (Points.Count >= count)
                return Points;

            var currentPoint = Points[Points.Count - 1];
            var i = Points.Count;

            while (i < count)
            {
                var x = Center.X + (int)Math.Round(angle * Math.Cos(angle));
                var y = Center.Y + (int)Math.Round(angle * Math.Sin(angle));
                var nextPoint = new Point(x, y);

                if (!nextPoint.Equals(currentPoint))
                {
                    Points.Add(new Point(x, y));
                    i++;
                }

                angle += angleStep;
                currentPoint = nextPoint;
            }

            return Points;
        }
    }
}