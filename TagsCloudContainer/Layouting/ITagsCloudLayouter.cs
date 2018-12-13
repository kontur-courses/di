using System.Drawing;
using TagsCloudContainer.TagsClouds;

namespace TagsCloudContainer.Layouting
{
    public interface ITagsCloudLayouter
    {
        ITagsCloud TagsCloud { get; }
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}