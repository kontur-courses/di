using System.Drawing;
using TagsCloudContainer.TagsClouds;

namespace TagsCloudContainer
{
    public class TagsCloudFactory : ITagsCloudFactory
    {
        public ITagsCloud CreateTagsCloud()
        {
            return new TagsCloud();
        }
    }
}