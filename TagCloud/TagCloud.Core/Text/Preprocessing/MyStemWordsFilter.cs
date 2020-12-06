using System.Collections.Generic;
using System.Linq;
using MyStem.Wrapper.Workers.Grammar;
using MyStem.Wrapper.Workers.Grammar.Parsing;
using MyStem.Wrapper.Workers.Grammar.Parsing.Models;

namespace TagCloud.Core.Text.Preprocessing
{
    public class MyStemWordsFilter : IWordFilter
    {
        private readonly IGrammarAnalyser analyser;
        private readonly IGrammarAnalysisParser parser;

        private const MyStemSpeechPart AllowedSpeechPath = MyStemSpeechPart.Verb;

        public MyStemWordsFilter(IGrammarAnalyser analyser, IGrammarAnalysisParser parser)
        {
            this.analyser = analyser;
            this.parser = parser;
        }

        public IEnumerable<string> GetValidWordsOnly(IEnumerable<string> words) =>
            analyser.GetRawResult(string.Join(" ", words))
                .Where(r => r
                    .ParseWith(parser)
                    .Entries
                    .Any(e => e.SpeechPart == AllowedSpeechPath))
                .Select(r => r.Text);
    }
}