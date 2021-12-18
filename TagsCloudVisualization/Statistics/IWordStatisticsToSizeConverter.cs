using System.Collections.Generic;
using ResultProject;

namespace TagsCloudVisualization.Statistics
{
    public interface IWordStatisticsToSizeConverter
    {
        public Result<IEnumerable<TagWordInfo>> Convert(IWordsStatistics statistics, uint topWordCount);
    }
}