using System;
using System.Drawing;
using TagCloud.Extensions;

namespace TagCloud
{
    public class SpiralPointGenerator
    {
        private double theta = 0;
        private double radius = 0;
        private readonly double thetaStep = Math.PI / 360;
        private readonly double radiusStep = 0.01;

        private readonly Size center;

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

        private void DoStep()
        {
            theta += thetaStep;
            radius += radiusStep;
        }
    }
}