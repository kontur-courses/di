using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class PointProvider : IPointProvider
    {
        private double angle, radius;
        private const double SpiralParameter = 0.01;
        private readonly Point center;

        public PointProvider(Point center)
        {
            if (center.X < 0 || center.Y < 0)
                throw new ArgumentException("X or Y of center was negative");

            this.center = center;
        }

        public Point GetPoint()
        {
            var x = (int)Math.Round(radius * Math.Cos(angle));
            var y = (int)Math.Round(radius * Math.Sin(angle));

            radius += SpiralParameter;
            angle += Math.PI / 180;

            return new Point(center.X - x, center.Y - y);
        }

    }
}
