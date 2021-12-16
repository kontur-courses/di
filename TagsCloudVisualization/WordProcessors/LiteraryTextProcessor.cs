using System.Collections.Generic;
using TagsCloudVisualization.WordValidators;

namespace TagsCloudVisualization.WordProcessors
{
    public class LiteraryTextProcessor : BaseWordsProcessor
    {
        public LiteraryTextProcessor(IEnumerable<ILiteraryWordProcessor> wordProcessors, IEnumerable<IWordValidator> wordValidators) 
            : base(wordProcessors, wordValidators)
        {
        }
    }
}