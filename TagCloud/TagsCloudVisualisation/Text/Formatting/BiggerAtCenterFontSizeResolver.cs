using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualisation.Text.Formatting
{
    public class BiggerAtCenterFontSizeResolver : IFontSizeResolver
    {
        private const int MinFontSize = 10;
        private const int Step = 3;

        public IDictionary<string, float> GetFontSizesForAll(WordWithFrequency[] allWords)
        {
            var log = (int) Math.Floor(Math.Log2(allWords.Length));
            var sizes = ToStack(
                Enumerable.Range(1, log)
                    .Select(i => (int) Math.Pow(2, i))
                    .Select((c, i) => (Count: c, Size: log * Step - i * Step + MinFontSize))
                    .Reverse()
            );

            var currentSize = sizes.Pop();
            var currentSizeUses = 0;
            var result = new Dictionary<string, float>();
            foreach (var word in allWords)
            {
                result.Add(word.Word, currentSize.Size);
                if (++currentSizeUses >= currentSize.Count)
                {
                    currentSize = sizes.Pop();
                    currentSizeUses = 0;
                }
            }

            return result;
        }

        private static Stack<T> ToStack<T>(IEnumerable<T> source) => new Stack<T>(source);
    }
}