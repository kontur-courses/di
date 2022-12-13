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
        
        public static Rectangle GetRectangle(this Point center, int x, int y)
        {
            int left = center.X - (x / 2);
            int right = center.Y - (y / 2);
            Point angle = new Point(left, right);
            return new Rectangle(angle, new Size(x, y));
        }

        public static Point GetCenter(this Rectangle rectangle)
        { 
            var center = new Point((rectangle.X + rectangle.Right) / 2, (rectangle.Y + rectangle.Bottom) / 2);
            return center;
        }
        public static Point GetTopLeftCorner(this Rectangle rectangle)
        { 
            var topLeftCorner = new Point(rectangle.X, rectangle.Y);
            return topLeftCorner;
        }

        public static double GetDiagonal(this Size rect)
        {
            return Math.Sqrt(rect.Width * rect.Width + rect.Height * rect.Height);
        }

        public static int GetSquare(this Rectangle rectangle)
        {
            return rectangle.Width * rectangle.Height;
        }

        public static void MoveTo(this Rectangle rectangle, int x, int y)
        {
            rectangle.X = x;
            rectangle.Y = y;
        }
    }
}