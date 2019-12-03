using System.Collections.Generic;
using TextConfiguration.WordFilters;
using TextConfiguration.WordProcessors;
using System.Linq;

namespace TextConfiguration
{
    public class TextPreprocessor
    {
        private readonly IWordFilter[] filters;
        private readonly IWordProcessor wordProcessor;

        public TextPreprocessor(IWordFilter[] filters, IWordProcessor wordProcessor)
        {
            this.filters = filters;
            this.wordProcessor = wordProcessor;
        }

        public List<string> PreprocessText(string text)
        {
            return text
                .Split()
                .Where(wrd => !filters.Any(fltr => fltr.ShouldFilter(wrd)))
                .Select(wrd => wordProcessor.ProcessWord(wrd))
                .ToList();
        }
    }
}
