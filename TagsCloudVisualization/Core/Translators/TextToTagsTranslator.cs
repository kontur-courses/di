using System;
using System.Collections.Generic;
using NHunspell;
using TagsCloudVisualization.Core.Normalizers;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization.Core.Translators
{
    public class TextToTagsTranslator
    {
        public IEnumerable<TagInfo> TranslateTextToTags(IEnumerable<string> text, HashSet<string> boringWords)
        {
            var normalizedWords = new RussianWordsNormalizer().GetNormalizedWords(text, boringWords,
                new Hunspell("ru.aff", "ru.dic"));
            var filteredWords = new WordsFilter().GetFilteredWords(normalizedWords);
            var frequencies = GetWordsFrequencies(filteredWords);

            foreach (var pair in frequencies)
            {
                Console.WriteLine($"{pair.Key}:{pair.Value}");
            }
            
            // ToDo рассчитать шрифт слов
            // ToDo сгенерировать раскладку слов
            // ToDo вернуть получившиеся теги

            return new List<TagInfo>();
        }

        private Dictionary<string, int> GetWordsFrequencies(IEnumerable<string> words)
        {
            var frequencies = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!frequencies.ContainsKey(word))
                {
                    frequencies[word] = 0;
                }

                frequencies[word]++;
            }

            return frequencies;
        }
    }
}