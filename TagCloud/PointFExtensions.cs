using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public static class PointFExtensions
    {
        public static float DistanceTo(this PointF from, PointF to)
        {
            var dx = to.X - from.X;
            var dy = to.Y - from.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        public static RectangleF GetRectangle(this PointF center, Size size)
        {
            return new RectangleF(
                new PointF(center.X - size.Width / 2f, center.Y - size.Height / 2f),
                size);
        }
    }
}