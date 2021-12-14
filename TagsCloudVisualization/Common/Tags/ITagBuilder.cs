using System.Collections.Generic;
using TagsCloudVisualization.Common.TextAnalyzers;

namespace TagsCloudVisualization.Common.Tags
{
    public interface ITagBuilder
    {
        public IEnumerable<Tag> GetTags(IList<WordStatistic> wordStatistics);
    }
}