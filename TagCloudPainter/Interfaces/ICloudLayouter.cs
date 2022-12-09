using System.Drawing;

namespace TagCloudPainter.Interfaces;

public interface ICloudLayouter
{
    Rectangle PutNextRectangle(Size rectangleSize);
}