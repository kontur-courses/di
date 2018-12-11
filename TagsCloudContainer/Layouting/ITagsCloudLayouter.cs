using System.Drawing;

namespace TagsCloudContainer.Layouting
{
    public interface ITagsCloudLayouter
    {
        ITagsCloud TagsCloud { get; }
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}