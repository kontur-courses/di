using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudLayout
{
    public static class PointExtensions
    {
        public static double GetSquaredDistanceTo(this Point p1, Point p2)
        {
            return (p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y);
        }

        public static double GetSquaredDistanceTo(this PointF p1, PointF p2)
        {
            return (p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y);
        }

        public static double GetMaxSquaredDistanceTo(this Point center, IEnumerable<Rectangle> rectangles)
        {
            return rectangles
                .Select(rect => rect.GetCenter().GetSquaredDistanceTo(center))
                .Max();
        }
    }
}
