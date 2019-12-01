using System.Drawing;

namespace TagsCloudContainer.CloudLayouters
{
    public interface ICloudLayoutingAlgorithm
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}