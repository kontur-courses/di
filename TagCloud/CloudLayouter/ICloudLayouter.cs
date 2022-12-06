using System.Drawing;

namespace TagCloudVisualizer.CloudLayouter;

public interface ICloudLayouter
{
    Rectangle PutNextRectangle(Size rectangleSize);
}