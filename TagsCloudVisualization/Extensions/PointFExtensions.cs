using System.Drawing;
using System.Numerics;

namespace TagsCloudVisualization.Extensions
{
    public static class PointFExtensions
    {
        public static Vector2 ToVector(this PointF p)
            => new Vector2(p.X, p.Y);

        public static Vector2 GetNormalToCenter(this PointF point, PointF center)
        {
            var direction = new Vector2(center.X - point.X,
                center.Y - point.Y);
            var length = direction.Length();

            return new Vector2(direction.X / length,
                direction.Y / length);
        }

        public static Vector2 GetOffset(this PointF first, PointF second)
            => new Vector2(first.X + second.X, first.Y + second.Y);
    }
}
