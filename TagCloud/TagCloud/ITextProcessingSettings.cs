using System.Collections.Generic;

namespace TagCloud
{
    public interface ITextProcessingSettings
    {
        HashSet<string> IncludeWords { get; }
        HashSet<string> IncludePos { get; }
    }
}