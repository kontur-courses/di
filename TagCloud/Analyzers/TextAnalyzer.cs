using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Analyzers
{
    public class TextAnalyzer : ITextAnalyzer
    {
        private readonly IEnumerable<IWordFilter> wordFilters;
        private readonly IEnumerable<IWordConverter> wordConverters;
        private readonly IFrequencyAnalyzer frequencyAnalyzer;

        public TextAnalyzer(IEnumerable<IWordFilter> wordFilters, IEnumerable<IWordConverter> wordConverters, IFrequencyAnalyzer frequencyAnalyzer)
        {
            this.wordFilters = wordFilters;
            this.wordConverters = wordConverters;
            this.frequencyAnalyzer = frequencyAnalyzer;
        }

        public Dictionary<string, int> Analyze(IEnumerable<string> words)
        {
            var convertedWords = wordConverters.Aggregate(words, 
                (current, converter) => converter.Convert(current));
            
            var filteredWords = wordFilters.Aggregate(convertedWords,
                (current, filter) => filter.Analyze(current));
            
            return frequencyAnalyzer.Analyze(filteredWords);
        }
    }
}
