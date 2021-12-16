using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.Statistics
{
    public class DefaultWordStatisticsToSizeConverter : IWordStatisticsToSizeConverter
    {
        private readonly float maxFontSize;

        public DefaultWordStatisticsToSizeConverter(float maxFontSize = 1000)
        {
            this.maxFontSize = maxFontSize;
        }
        
        public IEnumerable<TagWordInfo> Convert(IWordsStatistics statistics, int topWordCount)
        {
            var words = statistics.GetStatistics().ToList();

            var maxTagWeight = (double)words.First().Count;
            var minTagWeight = (double)words.Last().Count;

            foreach (var (word, count) in words.Take(topWordCount < 0 ? words.Count : topWordCount))
            {
                var fontSize = count <= minTagWeight
                    ? 1f
                    : (float)Math.Ceiling(maxFontSize * (count - minTagWeight) / (maxTagWeight - minTagWeight));
                if (words.First().Count == 1) fontSize = maxFontSize;
                yield return new TagWordInfo(word, fontSize);
            }
        }
    }
}