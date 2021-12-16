using System.Collections.Generic;
using System.Drawing;
using TagCloud.Drawing;
using TagCloud.Layout;

namespace TagCloud.PreLayout
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
            var words = WordScaler.GetWordsWithScaledFontSize(wordsWithFrequency, options.BaseFontSize,
                options.FontFamily);

            using var g = Graphics.FromImage(new Bitmap(1, 1));
            foreach (var word in words)
            {
                var wordSize = g.MeasureString(word.Text, word.Font).ToSize();
                word.Rectangle = _layouter.PutNextRectangle(wordSize);
            }

            _layouter.Reset();
            return words;
        }
    }
}