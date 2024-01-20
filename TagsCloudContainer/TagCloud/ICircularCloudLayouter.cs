using System.Drawing;

namespace TagsCloudContainer.TagCloud;

public interface ICircularCloudLayouter
{
    Rectangle PutNextRectangle(Size rectangleSize);
}