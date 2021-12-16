using System.Collections.Generic;

namespace TagsCloudVisualization.Statistics
{
    public interface IWordStatisticsToSizeConverter
    {
        public IEnumerable<TagWordInfo> Convert(IWordsStatistics statistics, int topWordCount);
    }
}