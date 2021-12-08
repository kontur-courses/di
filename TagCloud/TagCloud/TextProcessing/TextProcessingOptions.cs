using System.Collections.Generic;
using System.Linq;
using CommandLine;

namespace TagCloud.TextProcessing
{
    [Verb("process", HelpText = "Process text from path")]
    public class TextProcessingOptions : ITextProcessingOptions
    {
        private static HashSet<string> DefaultExcludeWords => new() {"быть", "сказать", "мочь"};
        private static HashSet<PartOfSpeech> DefaultExcludePartOfSpeech => new()
        {
            PartOfSpeech.CONJ,
            PartOfSpeech.PART,
            PartOfSpeech.NUM,
            PartOfSpeech.PR,
            PartOfSpeech.ANUM,
            PartOfSpeech.APRO,
            PartOfSpeech.SPRO
        };

        private HashSet<string> _excludeWords;
        private HashSet<PartOfSpeech> _excludePartOfSpeech;


        [Option('p',"path", Required = true, HelpText = "Set file paths", Separator = ':')]
        public IEnumerable<string> FilesToProcess { get; set; }
        
        [Option('i', "include", Required = false, HelpText = "Set include words", Separator = ':')]
        public IEnumerable<string> IncludeWords { get; set; }
        
        [Option('e',"exclude", Required = false, HelpText = "Set words to exclude", Separator = ':')]
        public IEnumerable<string> ExcludeWords
        {
            get  => _excludeWords.Count != 0 ? _excludeWords : DefaultExcludeWords; 
            set => _excludeWords = value.ToHashSet();
        }

        [Option('a',"amount", Required = false, HelpText = "Set words amount in tag cloud", Default = 1000)]
        public int Amount { get; set; }
        
        [Option('s',"exclude-pos", Required = false, HelpText = "Part of speech to exclude", Separator = ':')]
        public IEnumerable<PartOfSpeech> ExcludePartOfSpeech
        {
            get => _excludePartOfSpeech.Count != 0 ? _excludePartOfSpeech : DefaultExcludePartOfSpeech;
            set => _excludePartOfSpeech = value.ToHashSet(); 
        }
    }
}