using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Extensions
{
    public static class RectangleExtensions
    {
        public static bool IntersectsWithPreviousRectangles(this Rectangle rectangle, List<Rectangle> rectangles)
        {
            for (var index = rectangles.Count - 1; index >= 0; index--)
            {
                if (rectangles[index].IntersectsWith(rectangle))
                    return true;
            }
            return false;
        }

    }
}