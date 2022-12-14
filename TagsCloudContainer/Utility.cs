using System;
using System.Drawing;

namespace TagsCloudContainer
{
    public static class Utility
    {
        public static Size MeasureString(this string s, Font font)
        {
            var fakeImage = new Bitmap(1, 1);
            var graphics = Graphics.FromImage(fakeImage);
            var sizeF = graphics.MeasureString(s, font);
            var size = Size.Ceiling(sizeF);
            return size;
        }

        public static Point GetCenter(this Rectangle rectangle)
        { 
            var center = new Point((rectangle.X + rectangle.Right) / 2, (rectangle.Y + rectangle.Bottom) / 2);
            return center;
        }
    }
}