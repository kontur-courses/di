using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ICloudLayout
    {
        Rectangle PutNextRectangle(Size rectangleSize, string text);
        List<ICloudTag> Rectangles { get; }
    }
}
