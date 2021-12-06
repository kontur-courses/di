using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public static class RectangleExtensions
    {
        public static Point GetCenter(this Rectangle rectangle) => 
            new(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2);

        public static bool IntersectsWith(this Rectangle rectangle, IEnumerable<Rectangle> rectangles) =>
            rectangles.Any(rectangle.IntersectsWith);
    }
}