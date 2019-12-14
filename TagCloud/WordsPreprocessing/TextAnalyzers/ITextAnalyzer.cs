using System.Collections.Generic;

namespace TagCloud.WordsPreprocessing.TextAnalyzers
{
    public interface ITextAnalyzer
    {
        Word[] GetWords(IEnumerable<string> words, int count);
    }
}