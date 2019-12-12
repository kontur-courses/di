using System.Drawing;

namespace TagsCloudContainer.CloudLayouter
{
    public interface ICloudLayouter
    {
        void SetCenter(Point center);
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}