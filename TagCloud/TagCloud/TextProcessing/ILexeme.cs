using System.Collections.Generic;

namespace TagCloud.TextProcessing
{
    public interface ILexeme
    {
        public string Text { get; }
        public Dictionary<string, string> Analysis { get; }
        public string Lemma { get; }
        public PartOfSpeech PartOfSpeech { get; }
    }
}