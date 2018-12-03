using System.Collections.Generic;
using System.Linq;
using TagCloud.Data;

namespace TagCloud.Parser
{
    public class WordsParser : IWordsParser
    {
        public IEnumerable<WordInfo> Parse(IEnumerable<string> words, HashSet<string> boringWords)
        {
            var occurrences = new Dictionary<string, int>();
            foreach (var word in words)
            {
                var lowerWord = word.ToLower();
                if (boringWords.Contains(lowerWord))
                    continue;
                if (occurrences.ContainsKey(lowerWord))
                    occurrences[lowerWord]++;
                else
                    occurrences[lowerWord] = 1;
            }
            return occurrences
                .Select(pair => new WordInfo(pair.Key, pair.Value))
                .OrderByDescending(info => info.Occurrences);
        }
    }
}