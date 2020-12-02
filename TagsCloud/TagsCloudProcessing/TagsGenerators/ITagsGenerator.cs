using System.Collections.Generic;
using TagsCloud.TextProcessing;

namespace TagsCloud.TagsCloudProcessing.TegsGenerators
{
    public interface ITagsGenerator
    {
        IEnumerable<Tag> CreateTags(IEnumerable<WordInfo> words);
    }
}
