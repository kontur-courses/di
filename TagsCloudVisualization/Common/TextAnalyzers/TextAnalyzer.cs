using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Common.Stemers;
using TagsCloudVisualization.Common.WordFilters;

namespace TagsCloudVisualization.Common.TextAnalyzers
{
    public class TextAnalyzer : ITextAnalyzer
    {
        private readonly IStemer stemer;
        private readonly IWordFilter wordFilter;

        public TextAnalyzer(IStemer stemer = null, IWordFilter wordFilter = null)
        {
            this.stemer = stemer;
            this.wordFilter = wordFilter;
        }

        public List<WordStatistic> GetWordStatistics(string text)
        {
            return GetWordStatistics(ParseWords(text));
        }

        public List<WordStatistic> GetWordStatistics(IEnumerable<string> words)
        {
            var statistic = new Dictionary<string, int>();
            foreach (var word in Filter(words.Select(GetStem)))
            {
                if (!statistic.ContainsKey(word))
                    statistic[word] = 1;
                else
                    statistic[word]++;
            }

            return statistic.Select(stat => new WordStatistic(stat.Key, stat.Value)).ToList();
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

        private IEnumerable<string> Filter(IEnumerable<string> words)
        {
            return wordFilter != null ? wordFilter.Filter(words) : words;
        }
    }
}