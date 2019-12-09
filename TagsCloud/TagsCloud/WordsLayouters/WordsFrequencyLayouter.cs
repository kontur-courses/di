using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudGenerator.Attributes;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.WordsLayouters
{
    [Factorial("WordsFrequencyLayouter")]
    public class WordsFrequencyLayouter : IWordsLayouter
    {
        private const int MaxSize = 300;
        private const int MinSize = 10;

        private readonly IRectanglesLayouter rectanglesLayouter;
        private readonly ISettings settings;

        public WordsFrequencyLayouter(IRectanglesLayouter rectanglesLayouter, ISettings settings)
        {
            this.rectanglesLayouter = rectanglesLayouter;
            this.settings = settings;
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
            foreach (var (word, freq) in wordsFreq.OrderByDescending(p => p.freq))
            {
                var maxSymbolSize = Math.Max((int)(MaxSize * (double)freq / maxFreqCount), MinSize);
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