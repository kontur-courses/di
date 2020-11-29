using System.Drawing;

namespace RectanglesCloudLayouter.Interfaces
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}