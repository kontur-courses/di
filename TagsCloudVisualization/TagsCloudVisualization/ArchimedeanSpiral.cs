using System.Drawing;

namespace TagsCloudVisualization
{
    public class ArchimedeanSpiral
    {
        public readonly Point Center;

        public double DistanceFromCenter { get; set; }

        private readonly double spiralRatio;

        private double angle;

        public ArchimedeanSpiral(Point center, double spiralRatio = 0.1)
        {
            Center = center;
            this.spiralRatio = spiralRatio;
        }

        public Point GetNextPoint()
        {
            angle += spiralRatio;
            DistanceFromCenter += spiralRatio;
            return Geometry.PolarToCartesianCoordinates(DistanceFromCenter, angle);
        }
    }
}