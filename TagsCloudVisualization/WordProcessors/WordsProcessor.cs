using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.WordValidators;

namespace TagsCloudVisualization.WordProcessors
{
    public class WordsProcessor : ITextProcessor
    {
        private readonly IWordProcessor[] wordProcessors;
        private readonly IWordValidator[] wordValidators;
        
        public WordsProcessor(IWordProcessor[] wordProcessors, IWordValidator[] wordValidators)
        {
            this.wordProcessors = wordProcessors;
            this.wordValidators = wordValidators;
        }
        public IEnumerable<string> ProcessWords(IEnumerable<string> text)
        {
            return text
                .Where(x => wordValidators.All(v => v.Validate(x)))
                .Select(word => wordProcessors.Aggregate(word, (current, wordProcessor) => wordProcessor.ProcessWord(current)));
        }
    }
}