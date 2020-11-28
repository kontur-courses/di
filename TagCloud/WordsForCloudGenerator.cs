using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud
{
    public class WordsForCloudGenerator : IWordsForCloudGenerator
    {
        private readonly string fontName;
        private readonly Color fontColor;
        private readonly int maxFontSize;


        public WordsForCloudGenerator(string fontName, Color color, int maxFontSize)
        {
            this.fontName = fontName;
            this.maxFontSize = maxFontSize;
            fontColor = color;
        }

        public List<WordForCloud> Generate(List<string> words)
        {
            var wordFrequency = GetWordsFrequency(words)
                .OrderBy(x => x.Value)
                .Reverse()
                .ToList();

            var maxFrequency = wordFrequency.FirstOrDefault().Value;
            return wordFrequency
                .Select(x =>
                    GetWordForCloud(fontName,
                        maxFontSize,
                        fontColor,
                        x.Key,
                        x.Value,
                        maxFrequency))
                .ToList();
        }

        private static Dictionary<string, int> GetWordsFrequency(List<string> words)
        {
            var wordFrequency = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (wordFrequency.ContainsKey(word))
                    wordFrequency[word] += 1;
                else
                    wordFrequency[word] = 1;
            }

            return wordFrequency;
        }

        private static WordForCloud GetWordForCloud(string font, int maxWordSize, Color color, string word,
            int wordFrequency, int maxFrequency)
        {
            var wordFontSize = (int) (maxWordSize * ((double) wordFrequency / maxFrequency) + 0.6);
            var wordSize = new Size((int) (word.Length * (wordFontSize + 6) * 0.65), wordFontSize + 10);
            return new WordForCloud(font, wordFontSize, word, wordSize, color);
        }
    }
}