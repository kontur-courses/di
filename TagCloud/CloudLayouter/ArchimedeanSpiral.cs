using System;
using System.Drawing;

namespace TagCloud
{
    public class ArchimedeanSpiral
    {
        private const double DeltaAngle = 2 * Math.PI / 360;

        public Point CentralPoint { get; }

        private readonly double scaleFactor;

        private double angle = -DeltaAngle;

        public ArchimedeanSpiral(Point centralPoint, double scaleFactor = 1)
        {
            if (scaleFactor <= 0)
                throw new ArgumentException("scale factor must be more than zero");

            CentralPoint = centralPoint;
            this.scaleFactor = scaleFactor;
        }

        public Point GetNextPoint()
        {
            angle += DeltaAngle;

            var x = CentralPoint.X + (int)(scaleFactor * angle * Math.Cos(angle));

            var y = CentralPoint.Y + (int)(scaleFactor * angle * Math.Sin(angle));

            return new Point(x, y);
        }
    }
}
