using System.Drawing;

namespace TagCloudContainer.PointAlgorithm
{
    public class ArithmeticSpiral : IPointer
    {
        private readonly Point currentPoint;
        private double angle;
        private readonly double ellipsoidMultiplier, multiplier;

        public ArithmeticSpiral(Point startPoint, int ellipsoidMultiplier = 1, int densityMultiplier = 1)
        {
            currentPoint = startPoint;
            multiplier = densityMultiplier;
            this.ellipsoidMultiplier = ellipsoidMultiplier;
        }
        public Point GetNextPoint()
        {
            var nextPoint = new Point((int)(currentPoint.X + Math.Cos(angle) * angle * ellipsoidMultiplier),
                (int)(currentPoint.Y + Math.Sin(angle) * angle));
            angle += Math.PI / (360 * multiplier);
            return nextPoint;
        }
    }
}