using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NHunspell;
using TagsCloudVisualization.Core.Layouters;
using TagsCloudVisualization.Core.Normalizers;
using TagsCloudVisualization.Core.Spirals;
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
            var minFrequency = frequencies.Min(pair => pair.Value);

            var layouter = new SpiralRectangleCloudLayouter(new ArchimedeanSpiral(1, 0.05f));
            
            foreach (var pair in frequencies)
            {
                var fontSize = pair.Value - minFrequency + 10;
                var font = new Font("Arial", fontSize);
                var value = pair.Key;
                var rectSize = TextRenderer.MeasureText(value, font);
                var rect = layouter.PutNextRectangle(rectSize);
                yield return new TagInfo(value, font, rect);
            }
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