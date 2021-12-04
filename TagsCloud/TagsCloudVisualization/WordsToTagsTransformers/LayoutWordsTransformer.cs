using System;
using System.Collections.Generic;
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

        public IEnumerable<Tag> Transform(IEnumerable<string> word) => throw new NotImplementedException();
    }
}