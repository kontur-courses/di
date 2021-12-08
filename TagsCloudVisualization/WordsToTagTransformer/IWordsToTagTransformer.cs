using System.Collections.Generic;

namespace TagsCloudVisualization.WordsToTagTransformer
{
    public interface IWordsToTagTransformer
    {
        IEnumerable<Tag> Transform(IEnumerable<string> words);
    }
}