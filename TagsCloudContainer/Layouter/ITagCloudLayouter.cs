using System.Drawing;

namespace TagsCloudContainer.Layouter
{
    public interface ITagCloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
        Point Center { get; }
    }
}