using System.Drawing;

namespace TagCloudContainer.Infrastructure.Layouter;

public interface ICloudLayouter
{
    Rectangle PutNextRectangle(Size rectangleSize);
    Rectangle[] GetLayout();
}