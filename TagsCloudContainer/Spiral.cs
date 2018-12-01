using System;
using System.Drawing;

namespace TagsCloudContainer
{
    public class Spiral
    {
        private readonly Point center;
        private double spiralAngle;

        public Spiral(Point center)
        {
            this.center = center;
        }

        public Point GetNextPoint()
        {
            var x = center.X + (int)(spiralAngle * Math.Cos(spiralAngle));
            var y = center.Y + (int)(spiralAngle * Math.Sin(spiralAngle));

            spiralAngle++;

            return new Point(x,y);
        }

        public double GetCurrentSpiralAngle() => spiralAngle;
    }
}
