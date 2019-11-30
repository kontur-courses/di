using System;
using System.Drawing;

namespace TagsCloudContainer.Extensions
{
    public static class PointExtensions
    {
        public static double GetDistanceTo(this Point from, Point to)
        {
            var xDifferenceSquared = (to.X - from.X) * (to.X - from.X);
            var yDifferenceSquared = (to.Y - from.Y) * (to.Y - from.Y);
            return Math.Sqrt(xDifferenceSquared + yDifferenceSquared);
        }
    }
}