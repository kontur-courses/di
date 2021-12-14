using System;
using System.Drawing;

namespace TagsCloudVisualization.Extensions
{
    public static class PointExtensions
    {
        public static double GetDistance(this Point point1, Point point2)
        {
            return Math.Sqrt((point1.X - point2.X) * (point1.X - point2.X) +
                             (point1.Y - point2.Y) * (point1.Y - point2.Y));
        }
    }
}