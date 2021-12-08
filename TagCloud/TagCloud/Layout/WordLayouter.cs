using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Drawing;
using TagCloud.TextProcessing;

namespace TagCloud.Layout
{
    internal class WordLayouter
    {
        private readonly ICloudLayouter _layouter;
        private readonly Func<int, float, double> _scale = (freq, fontSize) =>
            (Math.Pow(freq - 1, 0.65) + 1) * fontSize;

        public WordLayouter(ICloudLayouter layouter)
        {
            _layouter = layouter;
        }

        public List<Word> Layout(IDrawerOptions options, Dictionary<string, int> wordsWithFrequency)
        {
            using var g = Graphics.FromImage(new Bitmap(1, 1));
            var listWords = new List<Word>();
            var minFontSize = double.MaxValue;
            foreach (var (word, frequency) in wordsWithFrequency)
            {
                var fontSize = _scale(frequency, options.BaseFontSize);
                minFontSize = Math.Min(minFontSize, fontSize);
                var font = new Font(options.FontFamily, (float)fontSize);
                listWords.Add(new Word(word, font));
            }
            
            foreach (var word in NormalizeFontSize(minFontSize, listWords))
            {
                var wordSize = g.MeasureString(word.Text, word.Font).ToSize();
                word.Rectangle = _layouter.PutNextRectangle(wordSize);
            }

            return listWords;
        }

        private IEnumerable<Word> NormalizeFontSize(double minSize, List<Word> words)
        {
            return words
                .Select(w => w.WithFontSize(w.Font.Size / minSize * 14));
        }
    }
}