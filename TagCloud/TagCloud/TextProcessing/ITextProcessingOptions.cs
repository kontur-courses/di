using System.Collections.Generic;

namespace TagCloud.TextProcessing
{
    public interface ITextProcessingOptions
    {
        IEnumerable<string> FilesToProcess { get; }
        IEnumerable<string> IncludeWords { get; }
        IEnumerable<string> ExcludeWords { get; }
        int Amount { get; }
        IEnumerable<PartOfSpeech> ExcludePartOfSpeech { get; }
    }
}