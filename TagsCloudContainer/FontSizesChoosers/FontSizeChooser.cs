using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TagsCloudContainer.WordsHandlers;

namespace TagsCloudContainer.FontSizesChoosers
{
    public class FontSizeChooser : IFontSizeChooser
    {
        private readonly int baseFont;
        private readonly double minFontSize = 0.2;
        private readonly double reducingCoefficient = 0.9;

        public FontSizeChooser(int fontSize)
        {
            baseFont = fontSize;
        }

        public IEnumerable<PrintedWordInfo> GetWordInfos(IEnumerable<WordInfo> words)
        {
            var sortedWords = words.OrderBy(x => -x.Repetitions);
            var sizeColor = (double)baseFont;

            foreach (var wordInfo in sortedWords)
            {
                yield return new PrintedWordInfo(wordInfo.Word, (int)sizeColor);
                sizeColor = GetNewFontSize(sizeColor);
            }
        }

        private double GetNewFontSize(double currentSize)
            => Math.Max(baseFont * minFontSize, reducingCoefficient * currentSize);
    }
}