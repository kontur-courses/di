using System;
using System.Linq;

namespace MyStemAdapter
{
    public class WordAnalysisResult
    {
        public string Lex { get; set; }
        public string Gr { get; set; }

        public PartOfSpeech PartOfSpeech => string.IsNullOrWhiteSpace(Gr)
            ? PartOfSpeech.Unknown
            : PartOfSpeechAbbreviations.Parse(Gr.Split(',').First());
    }
}
