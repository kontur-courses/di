using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.WordFormatters
{
    public class SimpleFormatter : IWordFormatter
    {
        private readonly IWordsWeighter wordsWeighter;
        private readonly Font font;
        private readonly Color color;
        private readonly bool frequentWordsAsHuge;
        private readonly float fontMultiplier;

        public SimpleFormatter(
            IWordsWeighter wordsWeighter,
            IFormatterConfig config)
        {
            font = config.Font;
            color = config.Color;
            frequentWordsAsHuge = config.FrequentWordsAsHuge;
            fontMultiplier = config.FontMultiplier;
            this.wordsWeighter = wordsWeighter;
        }

        private readonly HashSet<string> usedWords = new HashSet<string>();

        public IEnumerable<Word> FormatWords(IEnumerable<string> words)
        {
            if (!words.Any())
            {
                yield break;
            }

            var wordsWeight = wordsWeighter.GetWordsWeight(words);
            var maxWeight = wordsWeight.Values.Max();

            foreach (var word in words)
            {
                if (!usedWords.Contains(word))
                {
                    usedWords.Add(word);
                    var fontSize = frequentWordsAsHuge
                        ? font.Size + wordsWeight[word] * fontMultiplier
                        : font.Size + (maxWeight - wordsWeight[word] + 1) * fontMultiplier;
                    var newFont = new Font(font.FontFamily, fontSize);

                    yield return new Word(newFont, color, word);
                }
            }
        }
    }
}