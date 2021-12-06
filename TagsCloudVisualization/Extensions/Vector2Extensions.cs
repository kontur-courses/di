using System.Drawing;
using System.Numerics;

namespace TagsCloudVisualization.Extensions
{
    public static class Vector2Extensions
    {
        public static PointF ToPointF(this Vector2 v) 
            => new PointF(v.X, v.Y);

        public static double GetDistanceTo(this Vector2 v, PointF p) 
            => (p.ToVector() - v).Length();
    }
}