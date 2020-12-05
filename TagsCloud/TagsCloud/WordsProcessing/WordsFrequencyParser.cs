using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloud.WordsProcessing
{
    public class WordsFrequencyParser : IWordsFrequencyParser
    {
        private HashSet<string> wordsToIgnore;
        public WordsFrequencyParser(IEnumerable<string> wordsToIgnore)
        {
            this.wordsToIgnore = wordsToIgnore.ToHashSet();
        }

        public Dictionary<string, int> ParseWordsFrequency(string fileName)
        {
            var frequencies = new Dictionary<string, int>();
            var words = File.ReadLines(fileName).Where(x => !wordsToIgnore.Contains(x)).Select(x => x.ToLower());
            foreach (var word in words)
                if (frequencies.ContainsKey(word))
                    frequencies[word]++;
                else
                    frequencies[word] = 1;
            return frequencies;
        }
    }
}