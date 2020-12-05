using System;
using System.Drawing;

namespace TagCloud.PointGetters
{
    internal class SpiralPointGetter : IPointGetter
    {
        public Point Center { get; }

        public double Gomotation { get; }

        private double radius;

        public SpiralPointGetter(Point center, double gomotation)
        {
            Center = center;
            Gomotation = gomotation;
        }

        public Point GetNextPoint()
        {
            var x = (int)(radius * Math.Cos(radius));
            var y = (int)(radius * Math.Sin(radius));
            ChangePolarCoordinate();
            return new Point(x + Center.X, y + Center.Y);
        }

        private void ChangePolarCoordinate() => radius += Gomotation;
    }
}
