using System.Collections.Generic;
using System.Linq;
using TagCloud.Data;

namespace TagCloud.Counter
{
    public class WordsCounter : IWordsCounter
    {
        public IEnumerable<WordInfo> GetWordsInfo(IEnumerable<string> words)
        {
            var occurrences = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (occurrences.ContainsKey(word))
                    occurrences[word]++;
                else
                    occurrences[word] = 1;
            }
            var maxOccurrences = occurrences.Max(pair => pair.Value);
            return occurrences
                .Select(pair => new WordInfo(pair.Key, pair.Value, (float) pair.Value / maxOccurrences))
                .OrderByDescending(info => info.Occurrences);
        }
    }
}