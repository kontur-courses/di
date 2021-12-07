using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Extensions;

namespace TagCloud
{
    public class WordLayouter
    {
        private readonly ICloudLayouter _layouter;

        private readonly int _fontSize = 14;

        private readonly Func<int, int, double> scale = (freq, fontSize) =>
            (Math.Pow(freq - 1, 0.65) + 1) * fontSize;

        public WordLayouter(ICloudLayouter layouter)
        {
            _layouter = layouter;
        }

        public List<Word> Layout(Dictionary<string, int> wordsWithFrequency)
        {
            var g = Graphics.FromImage(new Bitmap(1, 1));
            var listWords = new List<Word>();
            var minFontSize = double.MaxValue;
            foreach (var (word, frequency) in wordsWithFrequency)
            {
                var fontSize = scale(frequency, _fontSize);
                minFontSize = Math.Min(minFontSize, fontSize);
                var font = new Font(FontFamily.GenericSerif, (float)fontSize);
                listWords.Add(new Word(word, font));
            }
            NormalizeFontSize(minFontSize, listWords);
            //listWords = listWords.Shuffle().ToList();
            foreach (var word in listWords)
            {
                var wordSize = g.MeasureString(word.Text, word.Font).ToSize();
                //var layouter = _layouterBuilder(Point.Empty);
                word.Rectangle = _layouter.PutNextRectangle(wordSize);
            }

            return listWords;
        }

        private void NormalizeFontSize(double minSize, List<Word> words)
        {
            words.ForEach(w => w.Font = new Font(w.Font.FontFamily, (float)(w.Font.Size / minSize * 14)));
        }
    }
}