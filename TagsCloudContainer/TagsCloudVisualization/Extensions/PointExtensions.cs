using System.Drawing;
using System.Numerics;

namespace TagsCloudVisualization.Extensions;

public static class PointExtensions
{
    public static Point WithOffset(this Point point, Size offset)
    {
        return new Point(point.X + offset.Width, point.Y + offset.Height);
    }

    public static double DistanceTo(this Point point, Point other)
    {
        var vector = new Vector2(other.X - point.X, other.Y - point.Y);
        return vector.Length();
    }
}