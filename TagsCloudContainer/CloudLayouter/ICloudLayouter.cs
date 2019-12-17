using System.Drawing;

namespace CloudLayouter
{
    public interface ICloudLayouter
    {
        void SetCenter(Point center);
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}