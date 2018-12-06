using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Painter;

namespace TagsCloudContainer.Preprocessing
{
    public class WordsPreprocessor
    {
        private readonly WordsPreprocessorSettings settings;

        public WordsPreprocessor(WordsPreprocessorSettings settings)
        {
            this.settings = settings;
        }

        private IEnumerable<string> ProcessWords(string[] words)
        {
            return words.Select(word => word.ToLower()).Where(word => word.Length > settings.BoringWordsLength && !settings.ExcludedWords.Contains(word));
                //.Where(word => settings.WordsWhich(word))
                //.Select(word => settings.WordsSelector(word));
        }

        private IEnumerable<WordInfo> OrderWordFrequencies(Dictionary<string, int> frequencies)
        {
            return frequencies
                .OrderByDescending(kv => kv.Value)
                .Select(kv => new WordInfo
                {
                    Word = kv.Key,
                    Frequency = kv.Value
                });
        }

        public IEnumerable<WordInfo> CountWordFrequencies(string[] words)
        {
            if (words == null)
                throw new ArgumentNullException(nameof(words));
            var wordsFrequencies = new Dictionary<string, int>();
            foreach (var word in ProcessWords(words))
            {
                wordsFrequencies.TryGetValue(word, out var value);
                wordsFrequencies[word] = value + 1;
            }

            return OrderWordFrequencies(wordsFrequencies);
        }
    }
}