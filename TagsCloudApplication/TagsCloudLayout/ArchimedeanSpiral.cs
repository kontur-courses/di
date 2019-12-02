using System;
using System.Drawing;

namespace TagsCloudLayout
{
    public class ArchimedeanSpiral
    {
        private readonly Point center;

        private readonly double stepLength;
        private readonly double angleShiftForEachPoint;
        private double currentSpiralAngle;

        public ArchimedeanSpiral(Point center, double stepLength, double angleShiftForEachPoint)
        {
            if (stepLength <= 0)
                throw new ArgumentException("Spiral step length must be a positive number!");
            if (angleShiftForEachPoint <= 0)
                throw new ArgumentException("Angle delta should be a positive number!");
            this.center = center;
            this.stepLength = stepLength;
            this.angleShiftForEachPoint = angleShiftForEachPoint;
        }

        public Point CalculateNextPoint()
        {
            var angle = currentSpiralAngle;
            var radiusLength = stepLength * angle / (2 * Math.PI);
            currentSpiralAngle += angleShiftForEachPoint;

            var x = (int)(radiusLength * Math.Cos(angle) + center.X);
            var y = (int)(radiusLength * Math.Sin(angle) + center.Y);
            return new Point(x, y);
        }
    }
}
