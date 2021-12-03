using System.Drawing;

namespace TagsCloudContainer.Interfaces;

public interface ICloudLayouter
{
    Rectangle PutNextRectangle(Size rectangleSize);
}
