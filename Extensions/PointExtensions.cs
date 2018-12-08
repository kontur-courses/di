using System;
using System.Drawing;

namespace Extensions
{
    public static class PointExtensions
    {
        public static double DistanceTo(this Point from, Point to)=>
            Math.Sqrt((to.X-from.X)*(to.X-from.X) + (to.Y-from.Y)*(to.Y-from.Y));

        public static Point AddHeight(this Point point, int height) =>
            new Point(point.X, point.Y + height);

        public static Point AddWidth(this Point point, int width) =>
            new Point(point.X + width, point.Y);
    }
}