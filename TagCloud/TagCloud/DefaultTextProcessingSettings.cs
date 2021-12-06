using System.Collections.Generic;

namespace TagCloud
{
    public class DefaultTextProcessingSettings : ITextProcessingSettings
    {
        public HashSet<string> IncludeWords { get; }
        public HashSet<string> IncludePos { get; }
    }
}