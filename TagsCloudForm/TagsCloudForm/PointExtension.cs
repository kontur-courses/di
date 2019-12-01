using System;
using System.Drawing;
namespace TagsCloudVisualization
{
    public static class PointExtension
    {
        public static double Distance(this Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
    }
}
