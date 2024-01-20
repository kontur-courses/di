using System.Drawing;

namespace TagCloud;

public interface ICircularCloudLayouter
{
    Rectangle PutNextRectangle(Size rectangleSize);
}