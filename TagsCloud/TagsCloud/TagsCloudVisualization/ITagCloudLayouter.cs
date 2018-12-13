using System.Collections.Generic;

namespace TagsCloud.TagsCloudVisualization
{
    public interface ITagCloudLayouter
    {
        List<Tag> GetTags(Dictionary<string, int> wordFrequency);
    }
}
