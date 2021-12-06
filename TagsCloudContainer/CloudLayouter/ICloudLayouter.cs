using System.Drawing;

namespace TagsCloudContainer.CloudLayouter
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}