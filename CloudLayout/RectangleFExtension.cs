using System.Drawing;

namespace CloudLayout
{
    public static class RectangleFExtension
    {
        public static PointF GetCenter(this RectangleF rectangle) => new((rectangle.X + rectangle.Right) / 2,
            (rectangle.Y + rectangle.Bottom) / 2);

        public static float GetArea(this RectangleF rectangle) => rectangle.Height * rectangle.Width;
    }
}