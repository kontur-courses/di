using System;
using System.Drawing;

namespace TagCloud
{
    public static class PointFExtensions
    {
        public static float DistanceTo(this PointF from, PointF to)
        {
            var dx = to.X - from.X;
            var dy = to.Y - from.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        public static RectangleF GetRectangle(this PointF center, SizeF size)
        {
            return new RectangleF(
                new PointF(center.X - size.Width / 2f, center.Y - size.Height / 2f),
                size);
        }
    }
}