using System.Drawing;

namespace TagsCloudVisualization
{
    public class ArchimedeanSpiral : ICirclePointLocator
    {
        private const double spiralRatio = 0.1;
        public double DistanceFromCenter { get; set; }
        public double Angle { get; set; }

        public Point GetNextPoint()
        {
            Angle += spiralRatio;
            DistanceFromCenter += spiralRatio;
            return Geometry.PolarToCartesianCoordinates(DistanceFromCenter, Angle);
        }
    }
}