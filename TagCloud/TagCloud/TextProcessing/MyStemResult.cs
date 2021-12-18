using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloud.TextProcessing
{
    internal class MyStemResult : ILexeme
    {
        private readonly List<Dictionary<string, string>> _analysis;

        private MyStemResult(List<Dictionary<string, string>> analysis, string text)
        {
            Text = text;
            _analysis = analysis;
        }

        public string Text { get; }

        public Dictionary<string, string> Analysis => _analysis
            .SelectMany(d => d.Select(pair => pair))
            .GroupBy(p => p.Key)
            .ToDictionary(p => p.Key, p => p.First().Value);

        public string Lemma => Analysis["lex"];
        public PartOfSpeech PartOfSpeech => (PartOfSpeech) Enum.Parse(typeof(PartOfSpeech), GetPosTag());

        public static MyStemResult FromDto(MyStemResultDto? dto)
        {
            return new MyStemResult(dto?.Analysis, dto?.Text);
        }

        private string GetPosTag()
        {
            var grammemes = Analysis["gr"];
            var splitters = new[] {',', '='};
            return grammemes.Split(splitters)[0];
        }
    }
}