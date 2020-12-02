using System.Collections.Generic;
using System.Linq;
using TagCloud.TextAnalyzer.WordFilters;
using TagCloud.TextAnalyzer.WordNormalizer;

namespace TagCloud.TextAnalyzer
{
    public class StandardAnalyzer : ITextAnalyzer
    {
        private IWordNormalizer normalizer;
        private HashSet<IWordFilter> filters;
        
        public StandardAnalyzer(IWordNormalizer normalizer, params IWordFilter[] filters)
        {
            this.normalizer = normalizer;
            this.filters = filters.ToHashSet();
        }
        
        public HashSet<TagInfo> GetTags(List<string> words)
        {
            var wordsToCount = GetWordsCounts(words);
            
            var minCount = wordsToCount.Values.ToList().Min();
            var maxCount = wordsToCount.Values.ToList().Max();
            
            var tags = wordsToCount
                .Select(wordToCount => 
                    new TagInfo(wordToCount.Key, 
                        GetProportion(wordToCount.Value, minCount, maxCount)))
                .ToHashSet();
            return tags;
        }

        private Dictionary<string, int> GetWordsCounts(List<string> words)
        {
            return words.Select(word => normalizer.Normalize(word))
                    .Where(word => filters.All(f => f.IsWordToExclude(word)))
                    .GroupBy(word => word)
                    .ToDictionary(group => group.Key,
                        group => group.Count());
        }

        private static double GetProportion(int value, int minValue, int maxValue)
        {
            return (double) (value - minValue) / (maxValue - minValue);
        }
    }
}