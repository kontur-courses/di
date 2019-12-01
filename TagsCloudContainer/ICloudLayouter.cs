using System.Drawing;

namespace TagsCloudContainer
{
    interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}
