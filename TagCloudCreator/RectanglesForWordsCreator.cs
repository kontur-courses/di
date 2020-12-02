using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CloudLayouters;

namespace TagCloudCreator
{
    public class RectanglesForWordsCreator
    {
        private static readonly int MininalFontSize = 10;
        private static readonly int MaximalFontSize = 60;

        internal static IEnumerable<DrawingWord> GetReadyWords(IEnumerable<(string, int)> wordsStatistic,
            BaseCloudLayouter layouter, FontFamily fontFamily)
        {
            var statistic = wordsStatistic.ToList();
            var maxCount = statistic.Max(x => x.Item2);
            var fonts = new Font[MaximalFontSize - MininalFontSize + 1];
            foreach (var (text, count) in statistic)
            {
                var fontSize = (int) ((MaximalFontSize - MininalFontSize) * ((double) count / maxCount) +
                                      MininalFontSize);
                // ReSharper disable once ConstantNullCoalescingCondition
                fonts[fontSize - MininalFontSize] ??= new Font(fontFamily, fontSize);
                var font = fonts[fontSize - 10];
                var size = Graphics.FromImage(new Bitmap(1, 1))
                    .MeasureString(text, font).ToSize();
                var word = new DrawingWord(text, font, layouter.PutNextRectangle(size).Location);
                yield return word;
            }
        }
    }
}