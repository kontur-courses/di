using System;
using System.Drawing;

namespace TagsCloudVisualization.Extensions
{
    public static class PointExtensions
    {
        public static double GetDistanceToPoint(this Point firstPoint, Point secondPoint)
        {
            return Math.Sqrt(Math.Pow(secondPoint.X - firstPoint.X, 2) + Math.Pow(secondPoint.Y - firstPoint.Y, 2));
        }
    }
}