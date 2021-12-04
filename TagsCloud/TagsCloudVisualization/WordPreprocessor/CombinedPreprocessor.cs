using System;
using System.Collections.Generic;

namespace TagsCloudVisualization.WordPreprocessor
{
    public class CombinedPreprocessor : IWordsPreprocessor
    {
        private readonly IEnumerable<IWordsPreprocessor> _preprocessors;

        public CombinedPreprocessor(IEnumerable<IWordsPreprocessor> preprocessors)
        {
            _preprocessors = preprocessors;
        }

        public IEnumerable<string> Process(IEnumerable<string> words) => throw new NotImplementedException();
    }
}