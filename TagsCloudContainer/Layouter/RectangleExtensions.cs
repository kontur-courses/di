using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.Layouter
{
    public static class RectangleExtensions
    {
        public static bool IntersectsWithAnyFrom(this Rectangle rect, IEnumerable<Rectangle> rectangles) =>
            rectangles.Any(currentRectangle => currentRectangle.IntersectsWith(rect));

        public static Rectangle MoveToCenter(this Rectangle rect)
        {
            var center = GetCenter(rect);
            return new Rectangle(2 * rect.X - center.X, 2 * rect.Y - center.Y, rect.Width, rect.Height);
        }

        public static Point GetCenter(this Rectangle rect) => 
            new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
    }
}