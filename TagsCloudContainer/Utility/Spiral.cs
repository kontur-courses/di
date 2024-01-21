using System.Drawing;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Utility
{
    public class Spiral : INextPointProvider
    {
        private readonly Point center;
        private readonly double angleStep;
        private readonly double radiusStep;
        private double angle;
        private double radius;


        public Point GetNextPoint()
        {
            var point = ConvertFromPolarToCartesian(angle, radius);
            point.Offset(center);
            angle += angleStep;
            radius += radiusStep;
            return point;
        }

        public Spiral(Point center, double angleStep, double radiusStep)
        {
            if (radiusStep <= 0 || angleStep <= 0)
            {
                throw new ArgumentException("Step values should be positive.");
            }

            this.center = center;
            this.angleStep = angleStep;
            this.radiusStep = radiusStep;
        }


        public IEnumerable<Point> GetPointsOnSpiral()
        {
            for (double angle = 0, radius = 0; ; angle += angleStep, radius += radiusStep)
            {
                var point = ConvertFromPolarToCartesian(angle, radius);
                point.Offset(center);
                yield return point;
            }
        }

        private static Point ConvertFromPolarToCartesian(double angle, double radius)
        {
            var x = (int)Math.Round(Math.Cos(angle) * radius);
            var y = (int)Math.Round(Math.Sin(angle) * radius);
            return new Point(x, y);
        }
    }
}
