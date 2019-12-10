using System;
using System.Drawing;

namespace TagsCloudVisualization.Logic
{
    public static class PointExtensions
    {
        public static double GetLength(this Point point)
        {
            return Math.Sqrt(point.X * point.X + point.Y * point.Y);
        }
    }
}