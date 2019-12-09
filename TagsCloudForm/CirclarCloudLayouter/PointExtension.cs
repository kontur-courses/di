using System;
using System.Drawing;

namespace CircularCloudLayouter
{
    public static class PointExtension
    {
        public static double Distance(this Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        public static double DistanceWithCompression(this Point a, Point b, int xCompression, int yCompression)
        {
            return Math.Sqrt(Math.Pow((a.X - b.X)/xCompression, 2) + Math.Pow((a.Y - b.Y)/yCompression, 2));
        }
    }
}
