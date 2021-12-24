using System.Collections.Generic;
using TagsCloudVisualization.WordValidators;

namespace TagsCloudVisualization.WordProcessors
{
    internal class LiteraryTextProcessor : BaseWordsProcessor
    {
        public LiteraryTextProcessor(IEnumerable<ILiteraryWordProcessor> wordProcessors, IEnumerable<IWordValidator> wordValidators) 
            : base(wordProcessors, wordValidators)
        {
        }
    }
}