using System.Collections.Generic;
using TagsCloud.WordProcessing;

namespace TagsCloud.Interfaces
{
    public interface ITagGenerator
    {
        IEnumerable<Tag> GenerateTag(IEnumerable<(string word, int frequency)> wordStatistics);
    }
}
