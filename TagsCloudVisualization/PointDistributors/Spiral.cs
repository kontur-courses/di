using System;
using System.Drawing;

namespace TagsCloudVisualization.PointDistributors
{
    public class Spiral : IPointDistributor
    {
        public Spiral()
        {
            step = 1;
            deltaAngle = 0.1;
            center = new Point(0, 0);
        }

        public Spiral(int step, Point center, double deltaAngle)
        {
            this.step = step;
            this.center = center;
            this.deltaAngle = deltaAngle;
        }

        private readonly int step;
        private double angle;
        private readonly double deltaAngle;
        private Point center;
        public bool centerOnPoint;

        public Point GetPosition()
        {
            if (!centerOnPoint)
            {
                centerOnPoint = true;
                return center;
            }

            angle += deltaAngle;

            var k = step / (2 * Math.PI);
            var radius = k * angle;

            var position = new Point(
                center.X + (int)(Math.Cos(angle) * radius),
                center.Y + (int)(Math.Sin(angle) * radius));

            return position;
        }
    }
}
