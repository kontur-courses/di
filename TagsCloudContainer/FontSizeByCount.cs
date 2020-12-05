using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TagsCloudContainer
{
    class FontSizeByCount : IFontSizeCalculator
    {
        private const float maxFontSize = 20;
        private const float minFontSize = 10;
        public IEnumerable<WordWithFont> CalculateFontSize(IEnumerable<string> words, string fontFamily)
        {
            var wordCounts = CountWords(words);
            return CalculateFontSizeForWords(wordCounts, fontFamily);
        }

        private IEnumerable<WordWithFont> CalculateFontSizeForWords(Dictionary<string, int> wordCounts, string fontFamily)
        {
            var maxCount = wordCounts.Values.Max();
            var minCount = wordCounts.Values.Min();
            foreach (var wordCount in wordCounts)
            {
                var fontSize = maxFontSize * (wordCount.Value - minCount) / (maxCount - minCount) + minFontSize;
                yield return new WordWithFont(wordCount.Key, new Font(fontFamily, fontSize));
            }
        }

        private Dictionary<string, int> CountWords(IEnumerable<string> words)
        {
            var wordCounts = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (wordCounts.ContainsKey(word))
                    wordCounts[word]++;
                else
                    wordCounts[word] = 0;
            }

            return wordCounts;
        }
    }
}
