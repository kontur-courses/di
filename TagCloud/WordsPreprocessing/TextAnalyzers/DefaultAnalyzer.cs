using System.Collections.Generic;
using System.Linq;

namespace TagCloud.WordsPreprocessing.TextAnalyzers
{
    public class DefaultAnalyzer : ITextAnalyzer
    {
        public Word[] GetWords(IEnumerable<string> words, int count)
        {
            return words.Select(w => new Word(w, SpeechPart.Noun){ Frequency = 1}).ToArray();
        }
    }
}