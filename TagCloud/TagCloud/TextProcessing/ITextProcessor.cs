using System.Collections.Generic;

namespace TagCloud.TextProcessing
{
    public interface ITextProcessor
    {
        IEnumerable<Dictionary<string, int>> GetWordsWithFrequency(ITextProcessingOptions options);
    }
}