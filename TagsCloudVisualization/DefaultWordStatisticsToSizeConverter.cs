using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization
{
    public class DefaultWordStatisticsToSizeConverter : IWordStatisticsToSizeConverter
    {
        private readonly float maxFontSize;
        private readonly IWordsStatistics statistics;

        public DefaultWordStatisticsToSizeConverter(float maxFontSize, IWordsStatistics statistics)
        {
            this.maxFontSize = maxFontSize;
            this.statistics = statistics;
        }
        
        public IEnumerable<TagWordInfo> Convert()
        {
            var words = statistics.GetStatistics().ToList();

            var maxTagWeight = (double)words.First().Count;
            var minTagWeight = (double)words.Last().Count;

            foreach (var (word, count) in words)
            {
                var fontSize = count <= minTagWeight
                    ? 1f
                    : (float)Math.Ceiling(maxFontSize * (count - minTagWeight) / (maxTagWeight - minTagWeight));
                yield return new TagWordInfo(word, fontSize);
            }
        }
    }
}