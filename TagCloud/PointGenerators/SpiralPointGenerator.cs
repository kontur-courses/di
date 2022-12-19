using System;
using System.Drawing;
using TagCloud.Extensions;

namespace TagCloud.PointGenerators
{
    public class SpiralPointGenerator : IPointGenerator
    {
        private double theta;
        private double radius;
        private readonly double thetaStep = Math.PI / 360;
        private readonly double radiusStep = 0.01;

        private readonly Size center;

        public delegate IPointGenerator Factory();

        public SpiralPointGenerator()
        {
            center = new Size();
        }

        public SpiralPointGenerator(Point center)
        {
            this.center = new Size(center);
        }

        public Point GetNextPoint()
        {
            var point = new Point(
                x: (int)(radius * Math.Cos(theta)),
                y: (int)(radius * Math.Sin(theta))
                ).ShiftTo(center);

            DoStep();

            return point;
        }

        public Point GetCenterPoint() =>
            new Point(center);

        private void DoStep()
        {
            theta += thetaStep;
            radius += radiusStep;
        }
    }
}