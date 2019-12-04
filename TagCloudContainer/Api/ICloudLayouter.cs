using System.Drawing;

namespace TagCloudContainer.Api
{
    public interface ICloudLayouter : ILayoutProvider
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}