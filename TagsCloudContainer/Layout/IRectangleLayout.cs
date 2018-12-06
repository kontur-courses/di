using System.Drawing;

namespace TagsCloudContainer.Layout
{
    public interface IRectangleLayout
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}