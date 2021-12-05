using System;
using System.Drawing;

namespace TagCloud.Layouters
{
    public static class PointExtensions
    {
        public static double DistanceTo(this Point sourcePoint, Point destinationPoint)
        {
            var dx = sourcePoint.X - destinationPoint.X;
            var dy = sourcePoint.Y - destinationPoint.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
