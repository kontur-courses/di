using System.Drawing;

namespace TagsCloudLayout.CloudLayouters
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}
