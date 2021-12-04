using System.Collections.Generic;

namespace TagsCloudVisualization.WordsToTagsTransformers
{
    public interface IWordsToTagsTransformer
    {
        IEnumerable<Tag> Transform(IEnumerable<string> word);
    }
}