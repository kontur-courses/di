using System.Drawing;

namespace TagCloud.Core.Layouting
{
    public interface ITagCloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}