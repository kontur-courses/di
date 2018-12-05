using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public static class IEnumerableRectangleExtension
    {
        public static int GetSurroundingCircleRadius(this IEnumerable<Rectangle> rectangles)
        {
            var enumerable = rectangles.ToList();
            if (enumerable.Any())
            {
                var first = enumerable.First();
                var center = first.Location.Add(new Point(first.Width/2, first.Height/2));
                return enumerable
                    .Select(rect => new Point(MathHelper.MaxAbs(rect.Left, rect.Right),
                        MathHelper.MaxAbs(rect.Top, rect.Bottom)))
                    .Select(point => point.GetDistanceTo(center)).Max();
            }
            return 0;
        }
    }
}
