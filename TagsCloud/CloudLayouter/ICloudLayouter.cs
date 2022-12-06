using System.Drawing;

namespace TagsCloud.CloudLayouter
{
    public interface ICloudLayouter<out T>
    {
        T PutNextRectangle(Size size);
    }
}