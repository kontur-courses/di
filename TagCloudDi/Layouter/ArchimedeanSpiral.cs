using System.Drawing;

namespace TagCloudDi.Layouter
{
    public class ArchimedeanSpiral(Point centralPoint, Settings settings)
    {
        private double angle { get; set; }
        private const double DeltaAngle = Math.PI / 180;
        private readonly int scale = settings.SpiralScale;

        public Point GetNextPoint()
        {
            var newX = (int)(centralPoint.X + scale * angle * Math.Cos(angle));
            var newY = (int)(centralPoint.Y + scale * angle * Math.Sin(angle));
            angle += DeltaAngle;

            return new Point(newX, newY);
        }
    }
}
