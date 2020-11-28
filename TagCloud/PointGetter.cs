using System;
using System.Drawing;

namespace TagCloud
{
    internal class PointGetter
    {
        private readonly Point center;
        private double angle;
        private int radius;
        private double dangle => Math.PI / (2 * radius);
        internal PointGetter(Point center) => this.center = center;
        internal Point GetNextPoint()
        {
            var x = (int)(radius * Math.Cos(angle));
            var y = (int)(radius * Math.Sin(angle));
            ChangePolarCoordinate();
            return new Point(x + center.X, y + center.Y);
        }
        private void ChangePolarCoordinate()
        {
            if (radius == 0 || angle > 2 * Math.PI)
            {
                radius++;
                angle = 0;
            }
            else
                angle += dangle;
        }
    }
}
