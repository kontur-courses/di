using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.WordsLayouters
{
    public class DefaultWordsLayouter : IWordsLayouter
    {
        private const int MaxSize = 300;
        private const int MinSize = 10;

        private readonly IRectanglesLayouter rectanglesLayouter;

        public DefaultWordsLayouter(IRectanglesLayouter rectanglesLayouter) =>
            this.rectanglesLayouter = rectanglesLayouter ?? throw new ArgumentNullException();

        public (string word, Font font, RectangleF wordRectangle)[] ArrangeWords(string[] words, string font)
        {
            if (words == null || font == null)
                throw new ArgumentNullException();
            var result = new List<(string word, Font font, RectangleF wordRectangle)>();
            if (words.Length == 0)
                return result.ToArray();
            IEnumerable<(string word, int freq)> wordsFreq = words
                .GroupBy(w => w)
                .Select(g => (g.Key, g.Count()));
            var maxFreqCount = wordsFreq.Max(p => p.freq);
            foreach (var (word, freq) in wordsFreq.OrderByDescending(p => p.freq))
            {
                var maxSymbolSize = (int)(MaxSize * (double)freq / maxFreqCount);
                maxSymbolSize = maxSymbolSize < MinSize ? MinSize : maxSymbolSize;
                var wordFont = new Font(font, maxSymbolSize);
                var rectangle = rectanglesLayouter.PutNextRectangle(new SizeF(word.Length * wordFont.SizeInPoints, wordFont.Height));
                result.Add((word, wordFont, rectangle));
            }
            return result.ToArray();
        }
    }
}