using System.Drawing;

namespace TagCloud.Extensions;

public static class PointExtension
{
    public static double GetDistanceTo(this Point first, Point second)
    {
        return Math.Sqrt((first.X - second.X) * (first.X - second.X) + (first.Y - second.Y) * (first.Y - second.Y));
    }
}