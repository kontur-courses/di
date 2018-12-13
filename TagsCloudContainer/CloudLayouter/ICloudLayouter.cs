using System.Drawing;

namespace TagsCloudContainer.CloudLayouter
{
    public interface ICloudLayouter
    {
        RectangleF PutNextRectangleF(SizeF rectangleSize);
    }
}