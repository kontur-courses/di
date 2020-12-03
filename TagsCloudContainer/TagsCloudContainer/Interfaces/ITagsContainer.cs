using System.Collections.Generic;

namespace TagsCloudContainer.TagsCloudContainer.Interfaces
{
    public interface ITagsContainer
    {
        public List<Tag> GetTags(string text);
    }
}