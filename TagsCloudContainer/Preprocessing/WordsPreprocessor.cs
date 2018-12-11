using NHunspell;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Preprocessing
{
    public class WordsPreprocessor
    {
        private readonly WordsPreprocessorSettings settings;

        private readonly Hunspell hunspell =
            new Hunspell(@"NHunspellDictionary\ru_RU.aff", @"NHunspellDictionary\ru_RU.dic");

        public WordsPreprocessor(WordsPreprocessorSettings settings)
        {
            this.settings = settings;
        }

        private IEnumerable<string> ProcessWords(IEnumerable<string> words)
        {
            var filtered = words
                .Select(word => word.ToLower())
                .Where(word => word.Length > settings.BoringWordsLength && !settings.ExcludedWords.Contains(word));
            if (!settings.BringInTheInitialForm)
                return filtered;
            filtered = filtered.Select(word => ToInitialForm(word));

            return filtered;
        }

        private string ToInitialForm(string word)
        {
            var firstForm = hunspell.Stem(word).FirstOrDefault();
            return firstForm ?? word;
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

        public IEnumerable<WordInfo> CountWordFrequencies(IEnumerable<string> words)
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