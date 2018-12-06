using System;
using System.Drawing;


namespace TagsCloudVisualization
{
    public class Spiral : IPolar
    {
        private readonly double step;
        private double angle;
        public Point Center { get; }

        public Spiral(Point center, double step, double angle)
        {
            this.step = step;
            this.angle = angle;
            Center = center;
        }

        public Point GetNextPoint()
        {
            var radius = step * angle;
            var x = Center.X + (int)(radius * Math.Cos(angle));
            var y = Center.Y + (int) (radius * Math.Sin(angle));
            angle++;
            return new Point(x, y);
        }
    }
}
