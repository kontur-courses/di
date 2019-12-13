using System;
using System.Drawing;

namespace TagsCloudVisualization.Utils
{
    public static class PointExtensions
    {
        public static double GetDistanceTo(this Point point1, Point point2)
        {
            return Math.Sqrt((point2.X - point1.X) * (point2.X - point1.X) +
                             (point2.Y - point1.Y) * (point2.Y - point1.Y));
        }
    }
}