using System.Drawing;

namespace RectanglesCloudLayouter.Interfaces
{
    public interface ICloudLayouter
    {
        int CloudRadius { get; }
        Point Center { get; }
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}