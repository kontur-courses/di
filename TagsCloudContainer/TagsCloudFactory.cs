using System.Drawing;

namespace TagsCloudContainer
{
    public class TagsCloudFactory : ITagsCloudFactory
    {
        public ITagsCloud CreateTagsCloud(Point center)
        {
            return new TagsCloud(center);
        }
    }
}