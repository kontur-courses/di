using System;
using System.Drawing;
using System.Numerics;

namespace TagsCloudVisualization.Extensions
{
    public static class RectangleFExtensions
    {
        public static PointF GetCenter(this RectangleF rect) =>
            new PointF(rect.X + rect.Width / 2f, 
                rect.Y + rect.Height / 2f);

        public static RectangleF GetRectangleByCenter(SizeF sz, PointF point) =>
            new RectangleF(new PointF
                (point.X - sz.Width / 2f, point.Y - sz.Height / 2f), sz);

        public static Vector2 GetRectanglesBoundsMaxOffset
            (this RectangleF first, RectangleF second)
        {
            var height = Math.Max(first.Top - second.Top, second.Bottom - first.Bottom);
            var width = Math.Max(first.Left - second.Left, second.Right - first.Right);
            return new Vector2(width, height);
        }
    }
}
