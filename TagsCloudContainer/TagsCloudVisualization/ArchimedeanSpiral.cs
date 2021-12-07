using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class ArchimedeanSpiral
    {
        private readonly Point center;
        private readonly double radius;

        public ArchimedeanSpiral(Point center = default, double radius = 1)
        {
            if (radius <= 0)
                throw new ArgumentException("Radius should be positive.", nameof(radius));

            this.center = center;
            this.radius = radius;
        }

        public Point GetPoint(int degree)
        {
            if (degree < 0)
                throw new ArgumentException("Degree can't be negative.", nameof(degree));

            var radians = degree * Math.PI / 180;
            var length = radius * radians;
            return new Point
            {
                X = (int)(length * Math.Cos(radians)) + center.X,
                Y = -(int)(length * Math.Sin(radians)) + center.Y
            };
        }
    }
}