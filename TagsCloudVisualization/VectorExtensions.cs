using System;

namespace TagsCloudVisualization
{
    public static class VectorExtensions
    {
        public static bool Equals(this Vector a, Vector b, double eps)
        {
            return Math.Abs(a.X - b.X) < eps && Math.Abs(a.Y - b.Y) < eps;
        }
    }
}