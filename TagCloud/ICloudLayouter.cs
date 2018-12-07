using System.Drawing;

namespace TagsCloud
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size size);
    }
}