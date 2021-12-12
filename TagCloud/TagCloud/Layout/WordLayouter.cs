using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Drawing;
using TagCloud.TextProcessing;

namespace TagCloud.Layout
{
    internal class WordLayouter : IWordLayouter
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
                listWords.Add(new Word(word, new Font(options.FontFamily, (float)fontSize)));
            }
            
            listWords = NormalizeFontSize(minFontSize, listWords);
            foreach (var word in listWords)
            {
                var wordSize = g.MeasureString(word.Text, word.Font).ToSize();
                word.Rectangle = _layouter.PutNextRectangle(wordSize);
            }

            return listWords;
        }

        private List<Word> NormalizeFontSize(double minSize, List<Word> words)
        {
            return words
                .Select(w => w.WithFontSize(w.Font.Size / minSize * 14))
                .ToList();
        }
    }
}