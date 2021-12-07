using System;
using System.Drawing;

namespace TagsCloudVisualizationTest
{
    public static class PointExtension
    {
        public static double MetricTo(this Point source, Point point)
        {
            return Math.Sqrt(Math.Pow(source.X - point.X, 2) 
                             + Math.Pow(source.Y - point.Y, 2));
        }
    }
}