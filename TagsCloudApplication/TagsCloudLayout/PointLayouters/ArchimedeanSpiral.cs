using System;
using System.Drawing;

namespace TagsCloudLayout.PointLayouters
{
    public class ArchimedeanSpiral : ICircularPointLayouter
    {
        private readonly double stepLength;
        private readonly double angleShiftForEachPoint;
        private double currentSpiralAngle;

        public Point Center { get; }

        public ArchimedeanSpiral(Point center, double stepLength = 0.1, 
            double angleShiftForEachPoint = 2 * Math.PI / 1000)
        {
            if (stepLength <= 0)
                throw new ArgumentException("Spiral step length must be a positive number!");
            if (angleShiftForEachPoint <= 0)
                throw new ArgumentException("Angle delta should be a positive number!");
            Center = center;
            this.stepLength = stepLength;
            this.angleShiftForEachPoint = angleShiftForEachPoint;
        }

        public Point CalculateNextPoint()
        {
            var angle = currentSpiralAngle;
            var radiusLength = stepLength * angle / (2 * Math.PI);
            currentSpiralAngle += angleShiftForEachPoint;

            var x = (int)(radiusLength * Math.Cos(angle) + Center.X);
            var y = (int)(radiusLength * Math.Sin(angle) + Center.Y);
            return new Point(x, y);
        }
    }
}
