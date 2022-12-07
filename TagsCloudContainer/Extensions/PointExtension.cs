using System.Drawing;

namespace TagsCloudContainer.Extensions;

public static class PointExtension
{
    public static double GetDistance(this Point pointFrom, Point pointTo)
    {
        return Math.Sqrt(Math.Pow(pointFrom.X - pointTo.X, 2) + Math.Pow(pointFrom.Y - pointTo.Y, 2));
    }
}