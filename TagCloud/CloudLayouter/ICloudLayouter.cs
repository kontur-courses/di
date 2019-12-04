using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.CloudLayouter
{
    interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);

        IEnumerable<Rectangle> Rectangles { get; }

        Rectangle CloudRectangle { get; }
    }
}
