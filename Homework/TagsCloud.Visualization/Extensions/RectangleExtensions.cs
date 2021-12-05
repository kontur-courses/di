using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloud.Visualization.Extensions
{
    public static class RectangleExtensions
    {
        public static bool IntersectsWith(this Rectangle rectangle, IEnumerable<Rectangle> other) =>
            other.Any(rectangle.IntersectsWith);

        public static Point GetCenter(this Rectangle rectangle) =>
            new(rectangle.Left + rectangle.Width / 2,
                rectangle.Top + rectangle.Height / 2);
    }
}