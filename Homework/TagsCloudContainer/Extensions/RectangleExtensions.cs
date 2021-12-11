using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.Extensions
{
    internal static class RectangleExtensions
    {
        internal static bool IntersectsWith(this Rectangle rectangle, IEnumerable<Rectangle> rectangles)
        {
            return rectangles.Any(r => r.IntersectsWith(rectangle));
        }
    }
}