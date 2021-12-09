using System.Drawing;

namespace TagCloud.Infrastructure.Layouter;

public static class PointExtensions
{
    public static double GetDistance(this Point p1, Point p2)
    {
        return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
    }
}