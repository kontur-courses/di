using System;
using System.Drawing;

namespace TagCloud.Core.LayoutAlgorithms
{
    public class ArchimedeanSpiral
    {
        private readonly PointF start;
        private double angle;
        private readonly double deltaAngle;
        private readonly double spiralOffset;

        public ArchimedeanSpiral(PointF start,
            int step = 5, double deltaAngle = Math.PI / 360)
        {
            this.start = start;
            this.angle = 0d;
            this.deltaAngle = deltaAngle;
            this.spiralOffset = step / (Math.PI * 2);
        }

        public PointF GetNextPoint()
        {
            var nextPoint = new PointF(
                (float) (angle * Math.Cos(angle) * spiralOffset + start.X),
                (float) (angle * Math.Sin(angle) * spiralOffset + start.Y)
            );
            angle += deltaAngle;

            return nextPoint;
        }
    }
}