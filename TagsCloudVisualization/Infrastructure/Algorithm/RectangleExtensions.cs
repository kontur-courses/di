using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization.Infrastructure.Algorithm
{
    public static class RectangleExtensions
    {
        public static bool AreIntersected(this Rectangle r1, Rectangle r2)
        {
            return r1.Bottom >= r2.Top && r1.Top <= r2.Bottom
                                       && r1.Left <= r2.Right && r1.Right >= r2.Left;
        }

        public static bool NestedIn(this Rectangle interior, Rectangle outer)
        {
            return outer.Left <= interior.Left && interior.Right <= outer.Right
                                               && outer.Top <= interior.Top && interior.Bottom <= outer.Bottom;
        }

        public static bool AreIntersectedAny(this Rectangle rectangle, List<Rectangle> rectangles)
        {
            return rectangles.Any(r => rectangle.AreIntersected(r));
        }
    }
}