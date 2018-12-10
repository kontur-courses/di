using System.Collections.Generic;

namespace TagsCloud.TagsCloudVisualization
{
    interface ITagCloudLayouter
    {
        List<Tag> GetTags(Dictionary<string, int> wordFrequency);
    }
}
