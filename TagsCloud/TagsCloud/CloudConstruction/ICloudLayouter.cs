using System.Drawing;

namespace TagsCloud.CloudConstruction
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}