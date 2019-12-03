using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudTextPreparation
{
    public class TextPreparer
    {
        private readonly TextPreparerConfig textPreparerConfig;

        public TextPreparer(TextPreparerConfig textPreparerConfig)
        {
            this.textPreparerConfig =
                textPreparerConfig ?? throw new ArgumentException("Text prepare config can't be null");
        }

        public List<FrequencyWord> GetWordsByFrequency(IEnumerable<string> words)
        {
            words = ApplyConfiguration(words);
            return ConvertWordsToTagCloudWords(words);
        }

        private IEnumerable<string> ApplyConfiguration(IEnumerable<string> words)
        {
            words = words.Select(l => l.ToLower());
            words = ExcludeWords(words);
            return words;
        }

        private IEnumerable<string> ExcludeWords(IEnumerable<string> words)
        {
            return words.Where(word => !textPreparerConfig.IsWordExcluded(word));
        }

        private static List<FrequencyWord> ConvertWordsToTagCloudWords(IEnumerable<string> words)
        {
            var frequencyDictionary = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (frequencyDictionary.ContainsKey(word))
                    frequencyDictionary[word]++;
                else
                    frequencyDictionary.Add(word, 1);
            }

            return frequencyDictionary.Select(v => new FrequencyWord(v.Key, v.Value)).ToList();
        }
    }
}