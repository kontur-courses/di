using System.Drawing;

namespace TagsCloudContainer.CloudLayouters;

public interface ICloudLayouter
{
    public Rectangle PutNextRectangle(Size rectangleSize);
}