using System.Collections.Generic;
using System.Linq;

namespace TagsCloud
{
    public class FrequencyDictionary
    {
        public Dictionary<string, double> GetFrequencyDictionary(IEnumerable<string> words)
        {
            var frequencyDictionary = new Dictionary<string, int>();
            var wordsList = words.ToList();
            foreach (var word in wordsList)
                if (frequencyDictionary.ContainsKey(word))
                    frequencyDictionary[word]++;
                else
                    frequencyDictionary[word] = 1;

            return NormalizedDictionary(frequencyDictionary, wordsList.Count);
        }

        private static Dictionary<string, double> NormalizedDictionary(Dictionary<string, int> frequencyDictionary,
            int wordsCount)
        {
            var normalizedFrequencyDictionary = new Dictionary<string, double>();
            foreach (var pair in frequencyDictionary)
            {
                var value = pair.Value * 1.0 / wordsCount * 100;
                normalizedFrequencyDictionary.Add(pair.Key, value);
            }

            return normalizedFrequencyDictionary;
        }
    }
}