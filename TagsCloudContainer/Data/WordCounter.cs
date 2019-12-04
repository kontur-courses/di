using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Data
{
    internal class WordCounter
    {
        internal static IEnumerable<Word> Count(string[] words)
        {
            var occurrences = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (occurrences.ContainsKey(word))
                    occurrences[word]++;
                else
                    occurrences[word] = 1;
            }

            return occurrences
                .Select(pair => new Word(pair.Key, pair.Value, (double) pair.Value / words.Length))
                .OrderByDescending(word => word.Occurrences);
        }
    }
}