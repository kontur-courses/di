using System;
using System.Drawing;

namespace TagsCloudVisualization.Layouter
{
    public static class PointExtension
    {
        public static Point Add(this Point point1, Point point2) =>
            new Point(point1.X + point2.X, point1.Y + point2.Y);

        public static int GetDistanceTo(this Point point1, Point point2) =>
            (int)Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
    }
}
