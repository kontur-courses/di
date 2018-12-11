using System.Collections.Generic;

namespace TagsCloudContainer.WordFormatters
{
    public interface IWordsWeighter
    {
        IDictionary<string, int> GetWordsWeight(IEnumerable<string> words);
    }
}