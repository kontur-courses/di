using System;
using System.Drawing;

namespace TagCloud.TagCloudVisualization.Extensions
{
    public static class RectangleExtension
    {
        public static int GetCircumcircleRadius(this Rectangle rect)
        {
            var mostDistantPoint = GetMostDistantPointFromCenter(rect);
            return (int)Math.Sqrt(Math.Pow(mostDistantPoint.X, 2) + Math.Pow(mostDistantPoint.Y, 2));
        }

        private static Point GetMostDistantPointFromCenter(Rectangle rect)
        {
            return new Point(Math.Max(rect.Left, rect.Right), Math.Max(rect.Top, rect.Bottom));
        }
    }
}