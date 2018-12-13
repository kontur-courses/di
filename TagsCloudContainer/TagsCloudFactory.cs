using System.Drawing;

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