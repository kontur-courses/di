using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualizationDI.Extensions
{
    internal static class RectangularExtensions
    {
        internal static RectangleWithWord MakeFakeWordRectangle(Size size, Word word = null)
        {
            var fakeLocation = Point.Empty;
            var rectangle = new Rectangle(fakeLocation, size);

            return new RectangleWithWord(rectangle, word);
        }
    }
}