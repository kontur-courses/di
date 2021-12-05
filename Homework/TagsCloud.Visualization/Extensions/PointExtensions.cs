using System.Drawing;

namespace TagsCloud.Visualization.Extensions
{
    public static class PointExtensions
    {
        public static bool IsOnTheSameAxisWith(this Point point, Point otherPoint) =>
            point.X == otherPoint.X || point.Y == otherPoint.Y;
    }
}