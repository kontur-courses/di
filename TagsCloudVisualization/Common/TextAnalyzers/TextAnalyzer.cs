using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Common.Stemers;
using TagsCloudVisualization.Common.WordFilters;

namespace TagsCloudVisualization.Common.TextAnalyzers
{
    public class TextAnalyzer : ITextAnalyzer
    {
        private readonly IStemer stemer;
        private readonly IWordFilter[] wordFilters;

        public TextAnalyzer(IStemer stemer = null, IWordFilter[] wordFilters = null)
        {
            this.stemer = stemer;
            this.wordFilters = wordFilters ?? new IWordFilter[0];
        }

        public Dictionary<string, int> GetWordStatistics(string text)
        {
            return GetWordStatistics(ParseWords(text));
        }

        public Dictionary<string, int> GetWordStatistics(IEnumerable<string> words)
        {
            var statistic = new Dictionary<string, int>();
            foreach (var word in words.Select(GetStem)
                .Where(word => wordFilters.All(filter => filter.IsValid(word))))
            {
                if (!statistic.ContainsKey(word))
                    statistic[word] = 1;
                else
                    statistic[word]++;
            }

            return statistic;
        }

        private static IEnumerable<string> ParseWords(string text)
        {
            return text.Split()
                .Select(TrimPunctuation)
                .Where(word => !string.IsNullOrWhiteSpace(word));
        }

        private static string TrimPunctuation(string word)
        {
            return word.Trim(' ', '.', ',', '"', '!', '?', '-', ':', ';', '<', '>', '\'', '(', ')', '[', ']', '{', '}');
        }

        private string GetStem(string word)
        {
            if (stemer != null &&
                stemer.TryGetStem(word, out var stem))
                return stem;

            return word;
        }
    }
}