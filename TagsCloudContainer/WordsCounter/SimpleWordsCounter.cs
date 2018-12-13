using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.WordsCounter
{
    public class SimpleWordsCounter : IWordsCounter
    {
        public IDictionary<string, int> GetWordsFrequency(IEnumerable<string> words)
        {
            return words
                .Where(x => x != string.Empty)
                .GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.Count());
        }
    }
}