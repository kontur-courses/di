using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TagsCloudContainer.Settings;
using TagsCloudContainer.WordsHandlers;

namespace TagsCloudContainer.FontSizesChoosers
{
    public class FontSizeChooser : IFontSizeChooser
    {
        private readonly int baseFontSize;
        private readonly double minFontSize = 0.2;
        private readonly double reducingCoefficient = 0.9;

        public FontSizeChooser()
        {
            baseFontSize = 30;
        }

        public IEnumerable<PrintedWordInfo> GetWordInfos(IEnumerable<WordInfo> words)
        {
            var sortedWords = words.OrderByDescending(x => x.Repetitions);
            var sizeColor = (double)baseFontSize;

            foreach (var wordInfo in sortedWords)
            {
                yield return new PrintedWordInfo(wordInfo.Word, (int)sizeColor);
                sizeColor = GetNewFontSize(sizeColor);
            }
        }

        private double GetNewFontSize(double currentSize)
            => Math.Max(baseFontSize * minFontSize, reducingCoefficient * currentSize);
    }
}