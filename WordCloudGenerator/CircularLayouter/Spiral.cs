using System;
using System.Drawing;

namespace WordCloudGenerator.CircularLayouter
{
    public class Spiral
    {
        private readonly Point center;
        private double angle;

        public Spiral(Point center)
        {
            this.center = center;
        }

        public Point GetNextPoint()
        {
            var x = (int) (angle * Math.Cos(angle) + center.X);
            var y = (int) (angle * Math.Sin(angle) + center.Y);
            angle += Math.PI / 180;
            return new Point(x, y);
        }
    }
}