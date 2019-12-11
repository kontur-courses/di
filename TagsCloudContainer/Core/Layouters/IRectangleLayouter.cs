using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Core.Layouters
{
    public interface IRectangleLayouter
    {
        IEnumerable<Rectangle> Rectangles { get; }

        Rectangle PutNextRectangle(Size rectangleSize);
    }
}