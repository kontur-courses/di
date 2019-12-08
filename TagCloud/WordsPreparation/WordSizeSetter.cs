using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Infrastructure;

namespace TagCloud.WordsPreparation
{
    public class WordSizeSetter : IWordSizeSetter
    {
        private readonly IWordCountSetter wordCountSetter;
        private readonly int error;

        public WordSizeSetter(IWordCountSetter wordCountSetter, int error)
        {
            this.wordCountSetter = wordCountSetter;
            this.error = error;
        }

        public IEnumerable<Word> GetSizedWords(IEnumerable<Word> words, Size pictureSize)
        {
            var countedWords = wordCountSetter.GetCountedWords(words).ToList();
            var scale = GetScale(countedWords, pictureSize);
            foreach (var word in countedWords)
            {
                var smallestSize = word.GetSmallestPossibleSize();
                var size = new Size(smallestSize.Width * scale * word.Count, smallestSize.Height * scale * word.Count);
                yield return word.WithSize(size);
            }
        }

        private int GetScale(IReadOnlyCollection<Word> countedWords, Size pictureSize)
        {
            var scale = Math.Min(
                GetDimensionScale(
                    countedWords,
                    w => w.GetSmallestPossibleSize().Height,
                    pictureSize.Height),
                GetDimensionScale(
                    countedWords,
                    w => w.GetSmallestPossibleSize().Width,
                    pictureSize.Width));
            if (scale == 0)
                throw new ArgumentException(
                    $"Picture size {pictureSize.Width}x{pictureSize.Height} is too small for this word set");
            return scale;
        }

        private int GetDimensionScale(IEnumerable<Word> countedWords, Func<Word, int> dimensionSelector, int dimension)
        {
            var minDimensionSum = countedWords
                .Select(w => dimensionSelector(w) * w.Count)
                .Sum();
            return (int)Math.Floor((double)dimension / (minDimensionSum + error));
        }
    }
}