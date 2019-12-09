using System.Drawing;

namespace TagCloud.Algorithm
{
    public interface ITagCloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}
