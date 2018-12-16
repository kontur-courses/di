using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Settings;
using TagsCloudContainer.WordsHandlers;

namespace TagsCloudContainer.FontSizesChoosers
{
    public class FontSizeChooser : IFontSizeChooser
    {
        private readonly IImageSettings imageSettings;
        private readonly double minFontSize = 0.2;
        private readonly double reducingCoefficient = 0.9;

        public FontSizeChooser(IImageSettings imageSettings)
        {
            this.imageSettings = imageSettings;
        }

        public IEnumerable<PrintedWordInfo> GetWordInfos(IEnumerable<WordInfo> words)
        {
            var sortedWords = words.OrderByDescending(x => x.Repetitions);
            var sizeColor = (double)BaseFontSize;

            foreach (var wordInfo in sortedWords)
            {
                yield return new PrintedWordInfo(wordInfo.Word, (int)sizeColor);
                sizeColor = GetNewFontSize(sizeColor);
            }
        }

        private double GetNewFontSize(double currentSize)
            => Math.Max(BaseFontSize * minFontSize, reducingCoefficient * currentSize);

        private int BaseFontSize 
            => imageSettings.BaseFontSize;
    }
}