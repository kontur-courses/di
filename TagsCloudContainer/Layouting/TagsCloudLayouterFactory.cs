using System.Drawing;
using TagsCloudContainer.TagsClouds;

namespace TagsCloudContainer.Layouting
{
    public class TagsCloudLayouterFactory : ITagsCloudLayouterFactory
    {
        public ITagsCloudLayouter CreateTagsCloudLayouter(Point center, ITagsCloudFactory tagsCloudFactory)
        {
            return new CircularCloudLayouter(center, tagsCloudFactory);
        }
    }
}