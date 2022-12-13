using System;
using System.Drawing;

namespace TagCloud.PointGenerator
{
    public class ArchimedeanSpiral : IPointGenerator
    {
        private const double DeltaAngle = 2 * Math.PI / 360;

        public Point CentralPoint { get; }

        private readonly double compressionRatio;

        private readonly double scaleFactor;

        private double angle = -DeltaAngle;

        public ArchimedeanSpiral(Point centralPoint, double compressionRatio = 1, double scaleFactor = 1)
        {
            if (scaleFactor <= 0)
                throw new ArgumentException("scale factor must be more than zero");

            if (compressionRatio <= 0)
                throw new ArgumentException("compression ratio must be more than zero");

            CentralPoint = centralPoint;
            this.compressionRatio = compressionRatio;
            this.scaleFactor = scaleFactor;
        }

        public ArchimedeanSpiral()
           : this(new Point(0, 0))
        {
        }

        public Point GetNextPoint()
        {
            angle += DeltaAngle;

            var x = CentralPoint.X + (int)(scaleFactor * angle * Math.Cos(angle));

            var y = CentralPoint.Y + (int)(compressionRatio * scaleFactor * angle * Math.Sin(angle));

            return new Point(x, y);
        }
    }
}
