using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordSizing
{
    public class FrequencyWordSizer : IWordSizer
    {
        public IEnumerable<SizedWord> GetSizedWords(IEnumerable<string> words, int minSize = 10, int step = 1)
        {
            if (minSize <= 0)
                throw new ArgumentException("Min size must be positive");
            if (step <= 0)
                throw new ArgumentException("Step size must be positive");
            var frequencyDictionary = GetFrequencyDictionary(words);
            var sortedFrequencyDictionary = frequencyDictionary.OrderBy(pair => pair.Value)
                .ToDictionary(pair => pair.Key, pair => pair.Value);
            var sizedWords = new List<SizedWord>();
            var currentSize = minSize;
            var currentFrequency = sortedFrequencyDictionary.First().Value;
            foreach (var word in sortedFrequencyDictionary)
            {
                if (word.Value > currentFrequency)
                {
                    currentFrequency = word.Value;
                    currentSize += step;
                }
                sizedWords.Add(new SizedWord(word.Key, currentSize));
            }

            return sizedWords;
        }

        private Dictionary<string, int> GetFrequencyDictionary(IEnumerable<string> words)
        {
            var frequencyDictionary = new Dictionary<string, int>();
            foreach (var word in words)
            {
                var lowerCaseWord = word.ToLower();
                if (frequencyDictionary.ContainsKey(lowerCaseWord))
                {
                    frequencyDictionary[lowerCaseWord]++;
                }
                else
                {
                    frequencyDictionary.Add(lowerCaseWord, 1);
                }
            }

            return frequencyDictionary;
        }
    }
}