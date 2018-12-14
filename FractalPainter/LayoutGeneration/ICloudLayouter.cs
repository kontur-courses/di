using System.Drawing;

namespace TagCloud.LayoutGeneration
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}