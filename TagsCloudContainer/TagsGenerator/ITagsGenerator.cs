using System.Collections.Generic;
using TagsCloudContainer.Tag;

namespace TagsCloudContainer.TagsGenerator
{
    public interface ITagsGenerator
    {
        IEnumerable<ITag> GenerateTags(IDictionary<string, int> wordsFrequency);
    }
}