using System;
using System.Drawing;

namespace TagsCloudVisualization.Extensions
{
    public static class PointFExtensions
    {
        public static float DistanceTo(this PointF from, PointF to)
        {
            return (float) Math.Sqrt((from.X - to.X) * (from.X - to.X) + (from.Y - to.Y) * (from.Y - to.Y));
        }
    }
}