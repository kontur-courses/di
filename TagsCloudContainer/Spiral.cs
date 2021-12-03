using System;
using System.Drawing;

namespace TagsCloudContainer
{
    public class Spiral
    {
        private readonly double spiralCoef;
        private readonly double angleDelta;
        private readonly Point center;
        private double currentAngle;

        private Spiral(Point center, double spiralCoef, double angleDelta)
        {
            this.center = center;
            this.spiralCoef = spiralCoef;
            this.angleDelta = angleDelta;
        }

        public static Spiral Create(Point center, double spiralCoef = 1, double angleDelta = Math.PI / 360)
        {
            if (spiralCoef <= 0)
                throw new ArgumentException("Spiral coefficient must be a positive number");
            return new Spiral(center, spiralCoef, angleDelta);
        }

        public Point GetNext()
        {
            var x = Math.Round(spiralCoef * currentAngle * Math.Cos(currentAngle)) + center.X;
            var y = Math.Round(spiralCoef * currentAngle * Math.Sin(currentAngle)) + center.Y;
            currentAngle += angleDelta;
            return new Point((int)x, (int)y);
        }
    }
}
