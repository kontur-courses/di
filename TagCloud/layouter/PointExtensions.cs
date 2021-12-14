using System;
using System.Drawing;

namespace TagCloud.layouter
{
    internal static class PointExtensions
    {
        internal static double DistanceTo(this Point source, Point other)
            => Math.Sqrt(Math.Pow(source.X - other.X, 2) + Math.Pow(source.Y - other.Y, 2));
    }
}