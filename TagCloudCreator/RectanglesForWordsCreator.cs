using System;
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

        internal static IEnumerable<Word> GetReadyWords(IEnumerable<(string, int)> wordsStatistic,
            BaseCloudLayouter layouter)
        {
            var statistic = wordsStatistic.ToList();
            var maxCount = statistic.Max(x => x.Item2);
            foreach (var (text, count) in statistic)
            {
                var fontSize = (MaximalFontSize - MininalFontSize) * (((double) count) / maxCount) + MininalFontSize;
                var font = new Font(FontFamily.GenericSerif, (float) fontSize);
                var size = Graphics.FromImage(new Bitmap(1, 1))
                    .MeasureString(text, font).ToSize();
                var word = new Word(text, font, layouter.PutNextRectangle(size).Location, size);
                yield return word;
            }
        }
    }
}