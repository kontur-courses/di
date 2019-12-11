using System;
using System.Drawing;

namespace TagsCloudGenerator.Infrastructure
{
    public static class PointUtils
    {
        private const double RadiansInOneDegree = Math.PI / 180;

        public static PointF FromPolar(float rho, float phi)
        {
            var phiInRadian = phi * RadiansInOneDegree;
            return new PointF(
                (float) (rho * Math.Cos(phiInRadian)),
                (float) (rho * Math.Sin(phiInRadian)));
        }

        public static double DistanceTo(this Point from, Point to)
        {
            return Math.Sqrt((from.X - to.X) * (from.X - to.X) +
                             (from.Y - to.Y) * (from.Y - to.Y));
        }
    }
}