using System.Drawing;

namespace TagCloud.Extensions
{
    public static class PointExtensions
    {
        public static Point ShiftTo(this Point point, Size direction) =>
            Point.Add(point, direction);
    }
}
