using System.Drawing;

namespace TagsCloudContainer.Interfaces
{
    public interface ICloudLayouter
    {
        RectangleF PutNextRectangleF(SizeF rectangleSize);
    }
}