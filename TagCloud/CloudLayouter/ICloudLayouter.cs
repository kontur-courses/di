using System.Drawing;

namespace TagCloud.CloudLayouter
{
    public interface ICloudLayouter
    {
        SizeF SizeF { get;}
        PointF Center { get; }
        RectangleF PutNextRectangle(Size rectangleSize);
        RectangleF[] GetCloud();
    }
}