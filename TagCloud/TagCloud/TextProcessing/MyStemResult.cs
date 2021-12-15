using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloud.TextProcessing
{
    internal class MyStemResult
    {
        public static MyStemResult FromDto(MyStemResultDto? dto)
        {
            return new MyStemResult(dto?.Analysis, dto?.Text);
        }

        private readonly List<Dictionary<string, string>> _analysis;
        private readonly string _text;

        private MyStemResult(List<Dictionary<string, string>> analysis, string text)
        {
            _text = text;
            _analysis = analysis;
        }

        public Dictionary<string, string> Analysis => _analysis
            .SelectMany(d => d.Select(pair => pair))
            .GroupBy(p => p.Key)
            .ToDictionary(p => p.Key, p => p.First().Value);

        public string Lemma => Analysis["lex"];
        public PartOfSpeech PartOfSpeech => (PartOfSpeech) Enum.Parse(typeof(PartOfSpeech), GetPosTag());

        private string GetPosTag()
        {
            var gramms = Analysis["gr"];
            var splitters = new[] {',', '='};
            return gramms.Split(splitters)[0];
        }
    }
}