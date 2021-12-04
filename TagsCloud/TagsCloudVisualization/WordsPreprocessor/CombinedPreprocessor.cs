using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordsPreprocessor
{
    public class CombinedPreprocessor : IWordsPreprocessor
    {
        private readonly IEnumerable<IWordsPreprocessor> _preprocessors;

        private CombinedPreprocessor(IEnumerable<IWordsPreprocessor> preprocessors)
        {
            _preprocessors = preprocessors ?? throw new ArgumentNullException(nameof(preprocessors));
        }

        public CombinedPreprocessor(params IWordsPreprocessor[] preprocessors) : this(preprocessors.AsEnumerable())
        {
        }

        public IEnumerable<string> Process(IEnumerable<string> words) =>
            _preprocessors.Aggregate(words, (current, processor) => processor.Process(current));
    }
}