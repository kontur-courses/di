using System.Drawing;
using TagsCloudVisualization.Services;

namespace TagsCloudVisualization.Logic
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