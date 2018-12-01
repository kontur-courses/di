using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.WordFormatters
{
    public class SimpleFormatter : IWordFormatter
    {
        private readonly Font font;
        private readonly Color color;

        public SimpleFormatter(Font font, Color color)
        {
            this.font = font;
            this.color = color;
        }

        private readonly HashSet<string> usedWords = new HashSet<string>();

        public IEnumerable<IFormattedWord> FormatWords(IEnumerable<string> words)
        {
            var wordsWeight = GetWordsWeight(words);

            foreach (var word in words)
            {
                if (!usedWords.Contains(word))
                {
                    usedWords.Add(word);
                    var fontSize =  font.Size + wordsWeight[word] * 7f;
                    var newFont = new Font(font.FontFamily, fontSize);

                    yield return new Word(newFont, color, word);
                }
            }
        }

        private IDictionary<string, int> GetWordsWeight(IEnumerable<string> words)
        {
            var wordsWeight = new Dictionary<string, int>();

            foreach (var word in words)
            {
                if (wordsWeight.ContainsKey(word))
                {
                    wordsWeight[word]++;
                }
                else
                {
                    wordsWeight[word] = 0;
                }
            }

            return wordsWeight;
        }
    }
}