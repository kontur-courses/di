using System.Drawing;

namespace TagCloudPainter.Layouters;

public interface ICloudLayouter
{
    Rectangle PutNextRectangle(Size rectangleSize);
}