using System.Drawing;

namespace TagsCloudVisualization.CloudLayouter
{
    public interface ICloudLayouter
    {
        SizeF SizeF { get;}
        PointF Center { get; }
        RectangleF PutNextRectangle(Size rectangleSize);
        RectangleF[] GetCloud();
    }
}