using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.CloudLayouter
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);

        IEnumerable<Rectangle> Rectangles { get; }
    }
}
