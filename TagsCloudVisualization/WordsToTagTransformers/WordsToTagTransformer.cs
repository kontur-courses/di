using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordsToTagTransformers
{
    public class WordsToTagTransformer : IWordsToTagTransformer
    {
        public IEnumerable<Tag> Transform(IEnumerable<string> words) =>
            words
                .GroupBy(s => s)
                .ToDictionary(x => x.Key, x => x.Count())
                .Select(pair => new Tag(pair.Value, pair.Key));
    }
}