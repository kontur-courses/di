using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloudContainer
{
    public static class LayoutExtension
    {
        public static Bitmap CreateSizedBitmap(this List<Rectangle> layout, Size size)
        {
            if (size != Size.Empty) return new Bitmap(size.Width, size.Height);

            var (bmpWidth, bmpHeight) = layout.GetLayoutDimensions();

            return new Bitmap(bmpWidth, bmpHeight);
        }

        public static (int width, int height) GetLayoutDimensions(this List<Rectangle> layout)
        {
            var maxX = layout.Select(r => r.Right).Max();
            var minX = layout.Select(r => r.Left).Min();
            var maxY = layout.Select(r => r.Bottom).Max();
            var minY = layout.Select(r => r.Top).Min();

            var bmpWidth = maxX - minX;
            var bmpHeight = maxY - minY;
            return (bmpWidth, bmpHeight);
        }
    }
}