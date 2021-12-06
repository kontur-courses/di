using System.Collections.Generic;

namespace TagsCloudVisualization.TextAnalization.Analyzer
{
    public interface IAnalyzer
    {
        IEnumerable<Word> GetAnalyzedWords(IEnumerable<string> words);
        //bool CheckWord(string word);
    }
}
