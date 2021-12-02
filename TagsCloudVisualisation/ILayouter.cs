using System.Drawing;

namespace TagsCloudVisualization.Abstractions;

public interface ILayouter
{
    Point Center { get; }

    Rectangle PutNextRectangle(Size rectangleSize);
}