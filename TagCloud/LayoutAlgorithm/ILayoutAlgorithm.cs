using System.Drawing;

namespace TagCloud.LayoutAlgorithm;

public interface ICloudLayouter
{
    Rectangle PutNextRectangle(Size rectangleSize);
}