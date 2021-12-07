using System.Collections.Generic;

namespace TagCloud
{
    public class DefaultTextProcessingSettings : ITextProcessingSettings
    {
        public HashSet<string> IncludeWords => new();
        public HashSet<string> ExcludeWords => new() {"быть"};
        public int Amount => 1000;
        public HashSet<PartOfSpeech> ExcludePartOfSpeeches => new()
        {
            PartOfSpeech.CONJ,
            PartOfSpeech.PART,
            PartOfSpeech.NUM,
            PartOfSpeech.PR,
            PartOfSpeech.ANUM,
            PartOfSpeech.APRO,
            PartOfSpeech.SPRO
        };
    }
}