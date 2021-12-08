using System.Drawing;

namespace TagsCloudContainer.Abstractions;

public interface ILayouter
{
    Rectangle PutNextRectangle(Size rectangleSize);
}