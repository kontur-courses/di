using System;
using System.Collections.Generic;
using System.Linq;
using TagCloud.Infrastructure;

namespace TagCloud.Visualization
{
    public class WordSizeSetter : IWordSizeSetter
    {
        private readonly float wordsCountCorrectionCoefficient;
        private readonly int minFontSize;

        public WordSizeSetter(float wordsCountCorrectionCoefficient, int minFontSize)
        {
            this.wordsCountCorrectionCoefficient = wordsCountCorrectionCoefficient;
            this.minFontSize = minFontSize;
        }

        public WordSizeSetter() : this(100f, 1) { }


        public IEnumerable<Word> GetSizedWords(IEnumerable<Word> words, PictureConfig pictureConfig)
        {
            var wordsList = words.ToList();
            var maxWord = wordsList.OrderByDescending(w => w.Count).First();
            var maxWordFontSize = GetMaxWordFontSize(maxWord, wordsList.Count, pictureConfig);
            var fontSizeCoefficient = Math.Max(wordsList.Count / wordsCountCorrectionCoefficient, 1) * maxWordFontSize / maxWord.Count;
            foreach (var word in wordsList)
            {
                var fontSize = word.Count * fontSizeCoefficient;
                var size = SizeUtils.GetWordBasedSize(word.Value, pictureConfig.FontFamily, fontSize);
                yield return word.SetSize(size).SetFontSize(fontSize);
            }
        }

        private float GetMaxWordFontSize(Word mostFrequentWord, int wordsCount, PictureConfig pictureConfig)
        {
            var currentSize = SizeUtils.GetWordBasedSize(
                mostFrequentWord.Value, pictureConfig.FontFamily, minFontSize);

            if (!SizeUtils.CanFillSizeWithSizes(
                pictureConfig.Size, currentSize, wordsCount))
                throw new ArgumentException(
                    $"Picture size {pictureConfig.Size.Width}x{pictureConfig.Size.Height} is too small for this word set");

            var currentFontSize = minFontSize;
            while (SizeUtils.CanFillSizeWithSizes(pictureConfig.Size, currentSize, wordsCount)
                   && 2 * currentSize.Width < pictureConfig.Size.Width)
            {
                currentFontSize++;
                currentSize =
                    SizeUtils.GetWordBasedSize(mostFrequentWord.Value, pictureConfig.FontFamily, currentFontSize);
            }

            return currentFontSize - 1;
        }
    }
}