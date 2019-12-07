using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public static class RectangleExtensions
    {
        public static double GetDistanceToPoint(this Rectangle rec, Point point)
        {
            return Math.Sqrt(Math.Pow(rec.X - point.X, 2) + 
                             Math.Pow(rec.Y - point.Y, 2));
        }
    }
}