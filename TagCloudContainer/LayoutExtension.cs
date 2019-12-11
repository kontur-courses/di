using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloudContainer
{
    public static class LayoutExtension
    {
        public static Bitmap CreateSizedBitmap(this List<Rectangle> layout)
        {
            int maxX = layout.Select(r => r.Right).Max();
            int minX = layout.Select(r => r.Left).Min();
            int maxY = layout.Select(r => r.Bottom).Max();
            int minY = layout.Select(r => r.Top).Min();

            int bmpWidth = maxX - minX;
            int bmpHeight = maxY - minY;

            return new Bitmap(bmpWidth, bmpHeight);
        }
    }
}