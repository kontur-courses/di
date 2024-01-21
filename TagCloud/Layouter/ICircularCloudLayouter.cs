using System.Drawing;

namespace TagCloud.Layouter;

public interface ICircularCloudLayouter
{
    Rectangle PutNextRectangle(Size rectangleSize);
}