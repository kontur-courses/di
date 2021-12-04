using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.CloudLayouter;

namespace TagsCloudVisualization.WordsToTagsTransformers
{
    public class LayoutWordsTransformer : IWordsToTagsTransformer
    {
        private readonly ILayouter _layouter;

        public LayoutWordsTransformer(ILayouter layouter)
        {
            _layouter = layouter;
        }

        public IEnumerable<Tag> Transform(IEnumerable<WordCount> words)
        {
            return words.Select(word => new Tag(word.Word, _layouter.PutNextRectangle(GetSize(word))));
        }

        private static Size GetSize(WordCount wordCount) => new(wordCount.Count, wordCount.Count);
    }
}