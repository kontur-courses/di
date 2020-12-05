using System.Collections.Generic;
using TagsCloudVisualization.Tags;
using TagsCloudVisualization.Words;

namespace TagsCloudVisualization.TagCloudBuilders
{
    public interface ITagCloudBuilder
    {
        List<Tag> Build(IEnumerable<Word> wordsFrequency);
    }
}