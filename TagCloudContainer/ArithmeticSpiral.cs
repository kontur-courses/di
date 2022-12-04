using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class ArithmeticSpiral
    {
        private int x, y;
        private double angle;
        private int constant;
        public ArithmeticSpiral(Point start, int constant = 1)
        {
            x = start.X;
            y = start.Y;
            this.constant = constant;

        }
        public Point GetPoint()
        {
            var nextPoint = new Point((int)(x + Math.Cos(angle) * angle * constant),
                (int)(y + Math.Sin(angle) * angle));
            angle += Math.PI / 360;
            return nextPoint;
        }
    }
}