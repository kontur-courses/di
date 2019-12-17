using System.Drawing;

namespace TagsCloudContainer
{
    public interface ITagsLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}