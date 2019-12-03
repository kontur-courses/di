using System;
using System.Drawing;

namespace TagsCloud.Extensions
{
    public static class PointExtension
    {
        public static double GetDistance(this Point currentPoint, Point otherPoint)
        {
            return Math.Sqrt(Math.Pow(currentPoint.X - otherPoint.X, 2) + Math.Pow(currentPoint.Y - otherPoint.Y, 2));
        }
    }
}
