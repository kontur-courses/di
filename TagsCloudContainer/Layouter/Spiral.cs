using System;
using System.Drawing;

namespace TagsCloudContainer.Layouter
{
    public class Spiral : IPositionGenerator
    {
        private readonly SpiralSettings settings;
        private double currentAngle;
        private int piFactor = 2;
        private double angleDelta;

        public Spiral(SpiralSettings settings)
        {
            this.settings = settings;
            angleDelta = settings.AngleDelta;
        }

        public Point GetNextPosition()
        {
            var x = settings.Center.X + (int)(settings.SpiralWidth * currentAngle * Math.Cos(currentAngle));
            var y = settings.Center.Y + (int)(settings.SpiralWidth * currentAngle * Math.Sin(currentAngle));
            currentAngle += angleDelta;
            if (!(currentAngle > piFactor * Math.PI))
                return new Point(x, y);
            piFactor *= 2;
            angleDelta *= 0.7;
            return new Point(x, y);
        }

        public Point GetCenter() => settings.Center;
    }
}