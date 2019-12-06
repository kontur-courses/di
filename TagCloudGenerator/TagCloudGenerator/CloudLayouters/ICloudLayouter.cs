using System.Drawing;

namespace TagCloudGenerator.CloudLayouters
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}