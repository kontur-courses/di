using System.Drawing;

namespace TagsCloudContainer.CloudLayouters
{
    interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}
