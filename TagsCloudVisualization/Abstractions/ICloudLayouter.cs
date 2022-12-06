using System.Drawing;

namespace TagsCloudVisualization.Abstractions;

public interface ICloudLayouter
{
    Rectangle PutNextRectangle(Size rectangleSize);
}