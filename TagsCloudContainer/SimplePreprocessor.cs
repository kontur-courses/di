using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer
{
    public class SimplePreprocessor : IPreprocessor
    {
        public IEnumerable<string> GetValidWords(IEnumerable<string> words)
        {
            var frequencyDictionary = GetFrequencyDictionary(words);
            var validWords = frequencyDictionary
                .OrderBy(pair => pair.Value)
                .Reverse()
                .Select(pair => pair.Key);

            return validWords;
        }

        private Dictionary<string, int> GetFrequencyDictionary(IEnumerable<string> words)
        {
            var frequencyDictionary = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!frequencyDictionary.ContainsKey(word))
                    frequencyDictionary[word] = 1;
                frequencyDictionary[word]++;
            }

            return frequencyDictionary;
        }
    }
}
