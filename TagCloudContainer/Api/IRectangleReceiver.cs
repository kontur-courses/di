using System.Drawing;

namespace TagCloudContainer.Api
{
    public interface IRectangleReceiver
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}