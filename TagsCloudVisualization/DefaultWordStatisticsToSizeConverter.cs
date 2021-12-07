using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization
{
    public class DefaultWordStatisticsToSizeConverter : IWordStatisticsToSizeConverter
    {
        private const double MaxFontSize = 60;
        
        public IEnumerable<TagWordInfo> Convert(IWordsStatistics statistics)
        {
            var words = statistics.GetStatistics()
                .OrderBy(x => x.Count)
                .Reverse()
                .ToList();

            var maxTagWeight = (double)words.First().Count;
            var minTagWeight = (double)words.Last().Count;

            foreach (var (word, count) in words)
            {
                var fontSize = count <= minTagWeight
                    ? 1
                    : Math.Ceiling(MaxFontSize * (count - minTagWeight) / (maxTagWeight - minTagWeight));
                yield return new TagWordInfo(word, fontSize);
            }
        }
    }
}