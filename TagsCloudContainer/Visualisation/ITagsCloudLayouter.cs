using System.Drawing;

namespace TagsCloudContainer.Visualisation
{
    public interface ITagsCloudLayouter
    {
        ITagsCloud TagsCloud { get; }
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}