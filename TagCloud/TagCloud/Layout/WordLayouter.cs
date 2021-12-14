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

        public WordLayouter(ICloudLayouter layouter)
        {
            _layouter = layouter;
        }

        public List<Word> Layout(IDrawerOptions options, Dictionary<string, int> wordsWithFrequency)
        {
            var words = GetWordsWithScaledFontSize(wordsWithFrequency, options.BaseFontSize, options.FontFamily);
            
            using var g = Graphics.FromImage(new Bitmap(1, 1));
            foreach (var word in words)
            {
                var wordSize = g.MeasureString(word.Text, word.Font).ToSize();
                word.Rectangle = _layouter.PutNextRectangle(wordSize);
            }

            return words;
        }

        private static List<Word> GetWordsWithScaledFontSize(Dictionary<string, int> wordsWithFrequency, float baseFontSize,
            FontFamily fontFamily)
        {
            var listWords = new List<Word>();
            var minFontSize = double.PositiveInfinity;

            foreach (var (word, frequency) in wordsWithFrequency)
            {
                var fontSize = Scale(frequency, baseFontSize);
                minFontSize = Math.Min(minFontSize, fontSize);
                listWords.Add(new Word(word, new Font(fontFamily, fontSize)));
            }

            return NormalizeFontSize(minFontSize, listWords).ToList();

            float Scale(int freq, float fontSize) => (float) (Math.Pow(freq - 1, 0.65) + 1) * fontSize;

            IEnumerable<Word> NormalizeFontSize(double minSize, IEnumerable<Word> words) 
                => words.Select(w => w.WithFontSize(w.Font.Size / minSize * 14));
        }
    }
}