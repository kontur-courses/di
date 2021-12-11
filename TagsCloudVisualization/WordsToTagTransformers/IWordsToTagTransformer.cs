using System.Collections.Generic;

namespace TagsCloudVisualization.WordsToTagTransformers
{
    public interface IWordsToTagTransformer
    {
        IEnumerable<Tag> Transform(IEnumerable<string> words);
    }
}