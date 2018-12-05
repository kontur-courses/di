using System;
using System.Drawing;


namespace TagsCloudVisualization
{
    public class Spiral
    {
        private readonly double step;
        private double angle;

        public Spiral(double step, double angle)
        {
            this.step = step;
            this.angle = angle;
        }

        public Point GetNextPoint(Point center)
        {
            var radius = step * angle;
            var x = center.X + (int)(radius * Math.Cos(angle));
            var y = center.Y + (int) (radius * Math.Sin(angle));
            angle++;
            return new Point(x, y);
        }
    }
}
