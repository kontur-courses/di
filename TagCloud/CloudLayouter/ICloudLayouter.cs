using System.Drawing;

namespace TagCloud.CloudLayouter
{
    public interface ICloudLayouter
    {
        int Count { get; }

        Rectangle PutNextRectangle(Size size);
    }
}