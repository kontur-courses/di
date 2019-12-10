using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.Bases
{
    public abstract class WordsLayouterBase : IWordsLayouter
    {
        private readonly IRectanglesLayouter rectanglesLayouter;
        private readonly ISettings settings;
        private readonly Func<IEnumerable<(string word, int freq)>, IEnumerable<(string word, int freq)>> getEnumerableOrder;
        private readonly Func<(int freq, int maxFreq), int> getMaxSymbolSize;

        public WordsLayouterBase(
            IRectanglesLayouter rectanglesLayouter,
            ISettings settings,
            Func<IEnumerable<(string word, int freq)>, IEnumerable<(string word, int freq)>> getEnumerableOrder,
            Func<(int freq, int maxFreq), int> getMaxSymbolSize)
        {
            this.rectanglesLayouter = rectanglesLayouter;
            this.settings = settings;
            this.getEnumerableOrder = getEnumerableOrder;
            this.getMaxSymbolSize = getMaxSymbolSize;
        }

        public (string word, float maxFontSymbolWidth, string fontName, RectangleF wordRectangle)[] ArrangeWords(
            string[] words)
        {
            if (words == null)
                throw new ArgumentNullException();
            var result = new List<(
                string word,
                float maxFontSymbolWidth,
                string fontName,
                RectangleF wordRectangle)>();
            if (words.Length == 0)
                return result.ToArray();
            rectanglesLayouter.Reset();
            var wordsFreq = CalculateWordsFrequency(words);
            var maxFreqCount = wordsFreq.Max(p => p.freq);
            foreach (var (word, freq) in getEnumerableOrder(wordsFreq))
            {
                var maxSymbolSize = getMaxSymbolSize((freq, maxFreqCount));
                using (var wordFont = new Font(settings.Font, maxSymbolSize))
                {
                    var rectangle = rectanglesLayouter.PutNextRectangle(
                        new SizeF((word.Length + 1) * wordFont.SizeInPoints,
                        wordFont.Height));
                    result.Add((word, maxSymbolSize, settings.Font, rectangle));
                }
            }
            return result.ToArray();
        }

        private static IEnumerable<(string word, int freq)> CalculateWordsFrequency(string[] words) =>
            words
            .GroupBy(w => w)
            .Select(g => (g.Key, g.Count()));
    }
}
