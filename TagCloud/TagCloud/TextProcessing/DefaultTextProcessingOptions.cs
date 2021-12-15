using System.Collections.Generic;
using System.Linq;

namespace TagCloud.TextProcessing
{
    public class DefaultTextProcessingOptions : ITextProcessingOptions
    {
        private static HashSet<PartOfSpeech> _excludePartOfSpeech = new()
        {
            PartOfSpeech.CONJ,
            PartOfSpeech.PART,
            PartOfSpeech.NUM,
            PartOfSpeech.PR,
            PartOfSpeech.ANUM,
            PartOfSpeech.APRO,
            PartOfSpeech.SPRO
        };

        private HashSet<string> _excludeWords = new() {"быть", "мочь", "сказать"};
        public IEnumerable<string> FilesToProcess { get; set; }
        public IEnumerable<string> IncludeWords { get; set; }

        public IEnumerable<string> ExcludeWords
        {
            get => _excludeWords;
            set => _excludeWords = value.ToHashSet();
        }

        public int Amount => 1000;

        public IEnumerable<PartOfSpeech> ExcludePartOfSpeech
        {
            get => _excludePartOfSpeech;
            set => _excludePartOfSpeech = value.ToHashSet();
        }
    }
}