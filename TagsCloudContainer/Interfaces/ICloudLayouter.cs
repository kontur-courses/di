using System.Drawing;

namespace TagsCloudContainer.Interfaces
{
    public interface ICloudLayouter
    {
        void SetCenter(Point center);
        RectangleF PutNextRectangleF(SizeF rectangleSize);
    }
}