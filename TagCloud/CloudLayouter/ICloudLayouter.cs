using System.Drawing;

namespace TagCloud.CloudLayouter
{
    public interface ICloudLayouter
    {
        Point CloudCenter { get; }

        Rectangle PutNextRectangle(Size rectangleSize);
    }
}
