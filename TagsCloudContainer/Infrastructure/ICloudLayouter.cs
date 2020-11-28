using System.Drawing;

namespace TagsCloudContainer.Infrastructure
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}