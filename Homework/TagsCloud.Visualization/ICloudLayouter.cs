using System.Drawing;

namespace TagsCloud.Visualization
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}