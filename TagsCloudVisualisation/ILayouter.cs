using System.Drawing;

namespace TagsCloudVisualization.Abstractions;

public interface ILayouter
{
    Rectangle PutNextRectangle(Size rectangleSize);
}