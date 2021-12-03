using System;
using System.Drawing;

namespace TagsCloudContainer
{
    public static class PointExtensions
    {
        public static double GetDistance(this Point p1, Point p2)
        {
            var xDifference = (p1.X - p2.X);
            var yDifference = (p1.Y - p2.Y);
            return Math.Sqrt(xDifference * xDifference
                + yDifference * yDifference);
        }
    }
}
