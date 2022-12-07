using System.Drawing;

namespace TagsCloudVisualization
{
    internal interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}