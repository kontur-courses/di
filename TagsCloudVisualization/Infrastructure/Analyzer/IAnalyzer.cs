using System.Collections.Generic;

namespace TagsCloudVisualization.Infrastructure.Analyzer
{
    public interface IAnalyzer
    {
        public IEnumerable<IWeightedWord> CreateWordFrequenciesSequence(IEnumerable<string> words);
    }
}