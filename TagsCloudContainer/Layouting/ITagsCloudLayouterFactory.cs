using System.Drawing;
using TagsCloudContainer.TagsClouds;

namespace TagsCloudContainer.Layouting
{
    public interface ITagsCloudLayouterFactory
    {
        ITagsCloudLayouter CreateTagsCloudLayouter(Point center, ITagsCloudFactory tagsCloudFactory);
    }
}