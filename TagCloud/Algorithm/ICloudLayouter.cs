using System.Drawing;

namespace TagCloud.Algorithm
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}
