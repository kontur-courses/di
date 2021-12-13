using System;
using System.Drawing;

namespace TagsCloudVisualization.Extensions
{
    public static class DrawingExtensions
    {
        public static PointF[] GetPoints(this RectangleF rectangle)
        {
            return new[]
            {
                rectangle.Location,
                PointF.Add(rectangle.Location, new SizeF(rectangle.Width, 0)),
                PointF.Add(rectangle.Location, rectangle.Size),
                PointF.Add(rectangle.Location, new SizeF(0, rectangle.Height))
            };
        }

        public static float DistanceTo(this PointF point, PointF other)
        {
            return (float)Math.Sqrt((point.X - other.X) * (point.X - other.X) + (point.Y - other.Y) * (point.Y - other.Y));
        }

        public static void DrawRectangle(this Graphics graphics, Pen pen, RectangleF rectangle)
        {
            graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }
    }
}