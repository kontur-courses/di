using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud.layouter
{
    internal static class RectangleExtensions
    {
        internal static bool IntersectsWith(this IEnumerable<Rectangle> source, Rectangle other)
            => source.Any(x => x.IntersectsWith(other));
    }
}