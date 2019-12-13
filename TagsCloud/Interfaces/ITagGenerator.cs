using System.Collections.Generic;
using TagsCloud.TagGenerators;

namespace TagsCloud.Interfaces
{
    public interface ITagGenerator
    {
        IEnumerable<Tag> GenerateTag(IEnumerable<(string word, int frequency)> wordStatistics);
    }
}
