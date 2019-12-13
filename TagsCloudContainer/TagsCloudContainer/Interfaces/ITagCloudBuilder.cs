using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface ITagCloudBuilder
    {
        IEnumerable<Tag> GetTagsCloud(string fileName);
    }
}