using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    internal class TextParser : ITextParser
    {
        public TextParser(IEnumerable<string> lineSource)
        {
            wordSource = ParseToWords(lineSource);
        }

        public IEnumerable<string> wordSource { get; }

        private IEnumerable<string> ParseToWords(IEnumerable<string> lineSource)
        {
            foreach (var line in lineSource)
            {
                var punctuation = line.Where(char.IsPunctuation).Distinct().ToArray();
                foreach (var word in line.Split().Select(x => x.Trim(punctuation)))
                {
                    yield return word.ToLower();
                }
            }
        }
    }
}