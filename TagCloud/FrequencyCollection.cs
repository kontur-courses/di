using System.Collections.Generic;
using System.Linq;

namespace TagsCloud
{
    public class FrequencyCollection : IFrequencyCollection
    {
        public ICollection<KeyValuePair<string, double>> GetFrequencyCollection(IEnumerable<string> words)
        {
            var frequencyDictionary = new Dictionary<string, int>();
            var wordsList = words.ToList();
            foreach (var word in wordsList)
                if (frequencyDictionary.ContainsKey(word))
                    frequencyDictionary[word]++;
                else
                    frequencyDictionary[word] = 1;

            var normalizedDictionary = NormalizedDictionary(frequencyDictionary, wordsList.Count);
            var orderedCollection = normalizedDictionary.OrderByDescending(x => x.Value);
            return orderedCollection.ToList();
        }

        private static ICollection<KeyValuePair<string, double>> NormalizedDictionary(
            Dictionary<string, int> frequencyDictionary,
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