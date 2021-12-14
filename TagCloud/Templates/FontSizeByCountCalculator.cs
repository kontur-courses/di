using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Templates
{
    public class FontSizeByCountCalculator : IFontSizeCalculator
    {
        private readonly float maxSize;
        private readonly float minSize;

        public FontSizeByCountCalculator(float minSize, float maxSize)
        {
            this.maxSize = maxSize;
            this.minSize = minSize;
        }

        public Dictionary<string, float> GetFontSizes(IEnumerable<string> words)
        {
            var wordToCount = GetWordsCount(words);
            var maxCount = wordToCount.Max(k => k.Value);
            var minCount = wordToCount.Min(k => k.Value);
            var wordToSize = new Dictionary<string, float>();
            var divider = (maxCount - minCount) / (maxSize - minSize);
            if (divider == 0)
            {
                divider = 1;
            }

            foreach (var w in wordToCount)
                wordToSize[w.Key] = (w.Value - minCount) / divider + minSize;
            return wordToSize;
        }

        private Dictionary<string, int> GetWordsCount(IEnumerable<string> words)
        {
            var wordToCount = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (wordToCount.ContainsKey(word))
                {
                    wordToCount[word] += 1;
                }
                else
                {
                    wordToCount[word] = 1;
                }
            }

            return wordToCount;
        }
    }

    public interface IFontSizeCalculator
    {
        Dictionary<string, float> GetFontSizes(IEnumerable<string> words);
    }
}