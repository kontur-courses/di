using System.Drawing;

namespace TagsCloudContainer.RectangleGenerator
{
    public interface IRectangleGenerator
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}