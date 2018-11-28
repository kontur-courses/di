using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ICloudLayouter
    {
        List<Rectangle> Rectangles { get; }
        ISpiral Spiral { get; }
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}
