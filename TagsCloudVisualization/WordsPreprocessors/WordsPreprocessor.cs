using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.WordsPreprocessors.Filters;
using TagsCloudVisualization.WordsPreprocessors.Preparers;

namespace TagsCloudVisualization.WordsPreprocessors
{
    public class WordsPreprocessor : IWordsPreprocessor
    {
        private readonly IEnumerable<IWordsPreparer> preparers;
        private readonly IEnumerable<IWordsFilter> filters;

        public WordsPreprocessor(IEnumerable<IWordsPreparer> preparers, IEnumerable<IWordsFilter> filters)
        {
            this.preparers = preparers;
            this.filters = filters;
        }

        public IEnumerable<string> Preprocess(IEnumerable<string> words)
        {
            var preparedWords = preparers
                .Aggregate(words, (current, preparer) => preparer.Prepare(current));
            return filters
                .Aggregate(preparedWords, (current, filter) => filter.Filter(current));
        }
    }
}