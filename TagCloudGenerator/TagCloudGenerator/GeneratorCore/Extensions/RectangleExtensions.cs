using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloudGenerator.GeneratorCore.Extensions
{
    public static class RectangleExtensions
    {
        public static Rectangle CreateMovedCopy(this Rectangle rectangle, Size offset) =>
            new Rectangle(rectangle.Location + offset, rectangle.Size);

        public static bool IntersectsWith(this Rectangle rectangle, IEnumerable<Rectangle> otherRectangles) =>
            otherRectangles.Any(otherRectangle => otherRectangle.IntersectsWith(rectangle));
    }
}