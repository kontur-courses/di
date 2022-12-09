using System.Drawing;

namespace TagCloud.LayoutAlgorithm;

public interface ILayoutAlgorithm
{
    Rectangle PutNextRectangle(Size rectangleSize);
}