using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloud
{
    public class MyStemResult
    {
        public List<Dictionary<string, string>> analysis;
        public string Text;

        public Dictionary<string, string> Analisys => analysis
            .SelectMany(d => d.Select(pair => pair))
            .GroupBy(p => p.Key)
            .ToDictionary(p => p.Key, p => p.First().Value);

        public string Lemma => Analisys["lex"];
        public PartOfSpeech Pos => (PartOfSpeech) Enum.Parse(typeof(PartOfSpeech), GetPosTag());

        private string GetPosTag()
        {
            var gramms = Analisys["gr"];
            var splitters = new[] {',', '='};
            return gramms.Split(splitters)[0];
        }
    }
}