namespace TagsCloudContainer.TagsClouds
{
    public class TagsCloudFactory : ITagsCloudFactory
    {
        public ITagsCloud CreateTagsCloud()
        {
            return new TagsCloud();
        }
    }
}