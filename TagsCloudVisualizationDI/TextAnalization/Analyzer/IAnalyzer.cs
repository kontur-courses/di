using System.Collections.Generic;

namespace TagsCloudVisualizationDI.TextAnalization.Analyzer
{
    public interface IAnalyzer
    {
        IEnumerable<Word> GetAnalyzedWords(IEnumerable<string> words);
    }
}
