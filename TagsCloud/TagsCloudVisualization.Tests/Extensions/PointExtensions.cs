using System;
using System.Drawing;

namespace TagsCloudVisualization.Tests.Extensions
{
    public static class PointExtensions
    {
        public static double DistanceTo(this Point pivot, Point other)
        {
            var dx = pivot.X - other.X;
            var dy = pivot.Y - other.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}