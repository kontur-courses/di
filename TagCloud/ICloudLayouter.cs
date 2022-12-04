using System.Drawing;

namespace TagCloud
{
    public interface ICloudLayouter
    {
        Point CloudCenter { get; }

        Rectangle PutNextRectangle(Size rectangleSize);
    }
}
