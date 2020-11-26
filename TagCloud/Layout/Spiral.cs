using System;
using System.Drawing;

namespace TagCloud
{
    internal class Spiral : ISpiral
    {
        private readonly Point center;
        private readonly int Step;
        private double currentAngle;
        public Spiral(Point center, int step = 1)
        {
            this.center = center;
            this.Step = step;
            this.currentAngle = 0;
        }

        public Point GetNextPoint()
        {
            var x = (int) Math.Round(Step * currentAngle * Math.Cos(currentAngle)) + center.X;
            var y = (int) Math.Round(Step * currentAngle * Math.Sin(currentAngle)) + center.Y;
            currentAngle += 0.05;
            return new Point(x, y);
        }
    }
}