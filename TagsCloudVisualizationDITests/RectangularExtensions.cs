using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualizationTests
{
    internal static class RectangularExtensions
    {
        internal static IEnumerable<Point> GetRectangleNodes(this Rectangle rectangle)
        {
            yield return new Point(rectangle.X, rectangle.Y);
            yield return new Point(rectangle.X + rectangle.Width, rectangle.Y);
            yield return new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);
            yield return new Point(rectangle.X, rectangle.Y + rectangle.Height);
        }
    }
}
