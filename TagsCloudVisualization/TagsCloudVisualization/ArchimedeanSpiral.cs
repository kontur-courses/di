using System.Drawing;

namespace TagsCloudVisualization
{
    public class ArchimedeanSpiral : ICirclePointLocator
    {
        public Point Center { get; }

        public double DistanceFromCenter { get; set; }

        private readonly double spiralRatio;

        private double angle;

        public ArchimedeanSpiral(ImageSettings settings, double spiralRatio = 0.1)
        {
            Center = settings.CloudCenter;
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