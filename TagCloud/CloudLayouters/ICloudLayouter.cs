using System.Drawing;

namespace TagCloud.CloudLayouters
{
    public interface ICloudLayouter
    {
        Point Center { get; }
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}
