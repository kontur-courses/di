using System.Collections.Generic;
using System.Linq;
using ResultProject;
using TagsCloudVisualization.WordValidators;

namespace TagsCloudVisualization.WordProcessors
{
    internal abstract class BaseWordsProcessor : ITextProcessor
    {
        private readonly IEnumerable<IWordProcessor> wordProcessors;
        private readonly IEnumerable<IWordValidator> wordValidators;

        protected BaseWordsProcessor(IEnumerable<IWordProcessor> wordProcessors, IEnumerable<IWordValidator> wordValidators)
        {
            this.wordProcessors = wordProcessors;
            this.wordValidators = wordValidators;
        }
        
        public Result<IEnumerable<string>> ProcessWords(IEnumerable<string> text)
        {
            var fixedText = text.ToList();
            if (!fixedText.Any()) return Enumerable.Empty<string>().AsResult();
            
            var validatedWords = wordValidators.Any() 
                ? fixedText.Where(x => wordValidators.All(v => v.Validate(x)))
                : fixedText;

            if (!wordProcessors.Any()) return validatedWords.AsResult();


            return validatedWords.AsResult()
                .ThenForEach(x =>
                {
                    var processedWord = x.AsResult();

                    foreach (var wordProcessor in wordProcessors)
                    {
                        if (!processedWord.IsSuccess) return processedWord;
                        processedWord = wordProcessor.ProcessWord(processedWord.GetValueOrThrow()!);
                    }

                    return processedWord;
                });
        }
    }
}