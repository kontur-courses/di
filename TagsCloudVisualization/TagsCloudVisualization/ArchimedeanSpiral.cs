using System.Drawing;

namespace TagsCloudVisualization
{
    public class ArchimedeanSpiral : ICirclePointLocator
    {
        public Point Center { get; }

        public double DistanceFromCenter { get; set; }

        private  const double spiralRatio = 0.1;

        private double angle;

        public Point GetNextPoint()
        {
            angle += spiralRatio;
            DistanceFromCenter += spiralRatio;
            return Geometry.PolarToCartesianCoordinates(DistanceFromCenter, angle);
        }
    }
}