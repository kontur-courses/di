using System.Collections.Generic;
using TagsCloudVisualization.WordValidators;

namespace TagsCloudVisualization.WordProcessors
{
    internal class OneWordByLineProcessor : BaseWordsProcessor
    {
        public OneWordByLineProcessor(IEnumerable<IWordByLineProcessor> wordProcessors, IEnumerable<IWordValidator> wordValidators) 
            : base(wordProcessors, wordValidators)
        {
        }
    }
}