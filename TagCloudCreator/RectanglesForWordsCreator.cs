using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CloudLayouters;

namespace TagCloudCreator
{
    public static class RectanglesForWordsCreator
    {
        private static readonly int MininalFontSize = 10;
        private static readonly int MaximalFontSize = 60;

        internal static IEnumerable<DrawingWord> GetReadyWords(IEnumerable<WordStatistic> wordsStatistic,
            BaseCloudLayouter layouter, FontFamily fontFamily, IWordPainter wordPainter)
        {
            var statistics = wordsStatistic.ToList();
            if (statistics.Count == 0)
                yield break;
            var maxCount = statistics.Max(x => x.Count);
            var fonts = new Font[MaximalFontSize - MininalFontSize + 1];
            var graphics = Graphics.FromImage(new Bitmap(1, 1));
            foreach (var statistic in statistics)
            {
                var fontSize = (int) ((MaximalFontSize - MininalFontSize) * ((double) statistic.Count / maxCount) +
                                      MininalFontSize);
                // ReSharper disable once ConstantNullCoalescingCondition
                fonts[fontSize - MininalFontSize] ??= new Font(fontFamily, fontSize);
                var font = fonts[fontSize - MininalFontSize];
                var size = wordPainter.GetWordSize(statistic.Word, font);
                var word = new DrawingWord(statistic.Word, font, layouter.PutNextRectangle(size).Location);
                yield return word;
            }
        }
    }
}