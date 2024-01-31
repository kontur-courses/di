using System.Drawing;

namespace TagCloudDi.Layouter
{
    public class ArchimedeanSpiral(Point centerPoint, Settings settings) : IPointGenerator
    {
        private double angle { get; set; }
        private const double DeltaAngle = Math.PI / 180;
        private readonly int scale = settings.SpiralScale;
        public Point CenterPoint => centerPoint;

        public Point GetNextPoint()
        {
            var newX = (int)(centerPoint.X + scale * angle * Math.Cos(angle));
            var newY = (int)(centerPoint.Y + scale * angle * Math.Sin(angle));
            angle += DeltaAngle;

            return new Point(newX, newY);
        }
    }
}
