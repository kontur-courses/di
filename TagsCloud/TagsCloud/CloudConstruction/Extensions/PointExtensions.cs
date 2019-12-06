using System;
using System.Drawing;

namespace TagsCloud.CloudConstruction.Extensions
{
    public static class PointExtensions
    {
        public static double CountDistanceTo(this Point point1, Point point2)
        {
            return Math.Sqrt(point1.X * point1.X + point2.Y * point2.Y);
        }

        public static Point SummPoint(this Point point1, Point point2)
        {
            return new Point(point1.X + point2.X, point1.Y + point2.Y);
        }

        public static Point MultiplyOnConst(this Point point1, int c)
        {
            return new Point(point1.X * c, point1.Y * c);
        }
    }
}