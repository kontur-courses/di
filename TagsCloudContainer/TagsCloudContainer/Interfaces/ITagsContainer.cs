using System.Collections.Generic;

namespace TagsCloudContainer.TagsCloudContainer.Interfaces
{
    public interface ITagsContainer
    {
        public List<ITag> GetTags(string text);
    }
}