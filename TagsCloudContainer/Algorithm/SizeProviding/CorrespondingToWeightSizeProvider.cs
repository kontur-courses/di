using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Extensions;

namespace TagsCloudContainer.Algorithm.SizeProviding
{
    public class CorrespondingToWeightSizeProvider : IWordSizeProvider
    {
        private readonly int error;

        public CorrespondingToWeightSizeProvider(int error)
        {
            this.error = error;
        }

        public CorrespondingToWeightSizeProvider() : this(4) { }

        public IEnumerable<Word> SetWordsSizes(IEnumerable<Word> words, Size pictureSize)
        {
            var lastWordSize = Size.Empty;
            var lastWordWeight = 0;
            var wordsList = words.ToList();
            foreach (var word in wordsList.OrderBy(w => w.Weight))
            {
                var area = lastWordSize == Size.Empty
                    ? GetFirstWordSizeArea(wordsList.Count, pictureSize)
                    : GetNextWordSizeArea(word.Weight, lastWordSize.GetArea(), lastWordWeight);

                word.Size = GetWordSizeByArea(area);
                yield return word;
                lastWordSize = word.Size;
                lastWordWeight = word.Weight;
            }
        }

        private int GetFirstWordSizeArea(int wordsCount, Size pictureSize)
        {
            return pictureSize.GetArea() / (wordsCount * error);
        }

        private int GetNextWordSizeArea(int weight, int lastWordSizeArea, int lastWordWeight)
        {
            return lastWordSizeArea * weight / lastWordWeight;
        }

        private Size GetWordSizeByArea(int area)
        {
            var height = (int)Math.Sqrt((double)area / 2);
            var width = height * 2;
            return new Size(width, height);
        }
    }
}