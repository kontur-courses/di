using System.Drawing;

namespace TagsCloudApp.LayOuter
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}
