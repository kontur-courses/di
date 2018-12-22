using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WordCloudImageGenerator.Layouting.Layouters
{
    public static class RectangleExtensions
    {
        public static Point GetRectangleCenter(this Rectangle rectangle) =>
            rectangle.Location + new Size(rectangle.Width / 2, rectangle.Height / 2);

        public static bool IntersectsWithRectangles(this Rectangle rectangle, IEnumerable<Rectangle> rectangles) =>
            rectangles.Any(r => r.IntersectsWith(rectangle));
    }
}