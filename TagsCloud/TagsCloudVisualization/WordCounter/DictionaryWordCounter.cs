using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordCounter
{
    public class DictionaryWordCounter : IWordCounter
    {
        public IEnumerable<WordCount> Count(IEnumerable<string> words)
        {
            var counter = new Dictionary<string, int>();
            foreach (var word in words) counter[word] = counter.GetValueOrDefault(word, 0) + 1;

            return counter.Select(pair => new WordCount(pair.Key, pair.Value));
        }
    }
}