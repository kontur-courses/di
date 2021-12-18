using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.WordValidators;

namespace TagsCloudVisualization.WordProcessors
{
    public abstract class BaseWordsProcessor : ITextProcessor
    {
        private readonly IEnumerable<IWordProcessor> wordProcessors;
        private readonly IEnumerable<IWordValidator> wordValidators;

        protected BaseWordsProcessor(IEnumerable<IWordProcessor> wordProcessors, IEnumerable<IWordValidator> wordValidators)
        {
            this.wordProcessors = wordProcessors;
            this.wordValidators = wordValidators;
        }
        
        public IEnumerable<string> ProcessWords(IEnumerable<string> text)
        {
            var fixedText = text.ToList();
            if (!fixedText.Any()) return Enumerable.Empty<string>();
            
            var validatedWords = wordValidators.Any() 
                ? fixedText.Where(x => wordValidators.All(v => v.Validate(x)))
                : fixedText;
            
            return wordProcessors.Any() 
                ? validatedWords.Select(word => wordProcessors.Aggregate(word, (current, wordProcessor) => wordProcessor.ProcessWord(current)))
                : validatedWords;
        }
    }
}