using System;
using System.Drawing;

namespace TagsCloud
{
    public class CircularSpiral : ICoordinateSequence
    {
        private readonly Point center;
        private readonly double step;
        private readonly double width;
        private double angle;

        public CircularSpiral(Point center, double width, double step)
        {
            this.center = center;
            this.width = width;
            this.step = step;
        }

        public Point GetNextCoordinate()
        {
            angle += step;
            var radius = width * angle;
            var x = (int) (radius * Math.Cos(angle)) + center.X;
            var y = (int) (radius * Math.Sin(angle)) + center.Y;
            return new Point(x, y);
        }
    }
}