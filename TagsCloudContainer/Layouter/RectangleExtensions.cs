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
            var halfWidth = rect.Width / 2;
            var halfHeight = rect.Height / 2;
            return new Rectangle(rect.X - halfWidth, rect.Y - halfHeight, rect.Width, rect.Height);
        }
    }
}