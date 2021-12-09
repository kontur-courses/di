using System.Drawing;

namespace TagCloud.Infrastructure.Layouter;

public interface ICloudLayouter
{
    Rectangle PutNextRectangle(Size rectangleSize);
    Rectangle[] GetLayout();
}