using System.Drawing;

namespace TagCloudGenerator.GeneratorCore.CloudLayouters
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}