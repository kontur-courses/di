using System.Collections.Generic;
using TagsCloudVisualization.Tags;

namespace TagsCloudVisualization.TagCloudBuilders
{
    public interface ITagCloudBuilder
    {
        List<Tag> Build(Dictionary<string, int> wordsFrequency);
    }
}