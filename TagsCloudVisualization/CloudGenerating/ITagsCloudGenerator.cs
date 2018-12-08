using System.Collections.Generic;
using TagsCloudVisualization.Utils;

namespace TagsCloudVisualization.CloudGenerating
{
    public interface ITagsCloudGenerator
    {
        IEnumerable<Tag> GenerateTagsCloud(Statistics wordsStatistics);
    }
}
