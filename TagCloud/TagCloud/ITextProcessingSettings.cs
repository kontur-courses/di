using System.Collections.Generic;

namespace TagCloud
{
    public interface ITextProcessingSettings
    {
        HashSet<string> IncludeWords { get; }
        HashSet<string> ExcludeWords { get; }
        int Amount { get; }
        HashSet<PartOfSpeech> ExcludePartOfSpeeches { get; }
    }
}