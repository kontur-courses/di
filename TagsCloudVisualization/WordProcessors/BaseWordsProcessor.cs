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
            var validated = text.Where(x => wordValidators.All(v => v.Validate(x))).ToList();
            foreach (var word in validated)
            {
                var processedWord = word;
                foreach (var wordProcessor in wordProcessors)
                {
                    processedWord = wordProcessor.ProcessWord(processedWord);
                }

                yield return processedWord;
            }
            // return text
            //     .Where(x => wordValidators.All(v => v.Validate(x)))
            //     .Select(word => wordProcessors.Aggregate(word, (current, wordProcessor) => wordProcessor.ProcessWord(current)));
        }
    }
}