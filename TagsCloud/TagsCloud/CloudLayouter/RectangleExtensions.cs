using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.CloudLayouter
{
    public static class RectangleExtensions
    {

        public static double CountMaxDistanceTo(this Rectangle rectangle, Point point)
        {
            var point1 = new Point(rectangle.Bottom, rectangle.Left);
            var point2 = new Point(rectangle.Bottom, rectangle.Right);
            var point3 = new Point(rectangle.Top, rectangle.Left);
            var point4 = new Point(rectangle.Top, rectangle.Right);
            return Math.Max(Math.Max(point.CountDistanceTo(point1), point.CountDistanceTo(point2)),
                Math.Max(point.CountDistanceTo(point3), point.CountDistanceTo(point4)));
        }

        public static bool IntersectsWithAny(this Rectangle rectangle, IEnumerable<Word> words)
        {
            foreach (var word in words)
                if (rectangle.IntersectsWith(word.PosRectangle))
                    return true;
            return false;
        }
    }
}
