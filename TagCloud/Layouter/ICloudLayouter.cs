using System.Drawing;

namespace TagCloud.Layouter
{
    public interface ICloudLayouter
    {
        int Count { get; }
        Rectangle PutNextRectangle(Size size);
    }
}