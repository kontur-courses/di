using System.Drawing;

namespace TagCloudDi.Layouter
{
    public class ArchimedeanSpiral(Point centralPoint, double scale = 1)
    {
        private double angle { get; set; }
        private const double DeltaAngle = Math.PI / 180;

        public Point GetNextPoint()
        {
            var newX = (int)(centralPoint.X + scale * angle * Math.Cos(angle));
            var newY = (int)(centralPoint.Y + scale * angle * Math.Sin(angle));
            angle += DeltaAngle;

            return new Point(newX, newY);
        }
    }
}
