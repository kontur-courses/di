using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface IWordStatisticsToSizeConverter
    {
        public IEnumerable<TagWordInfo> Convert(IWordsStatistics statistics, int topWordCount);
    }
}