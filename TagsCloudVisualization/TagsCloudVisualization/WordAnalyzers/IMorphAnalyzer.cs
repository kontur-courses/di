using System.Collections.Generic;

namespace TagsCloudVisualization.WordAnalyzers
{
    public interface IMorphAnalyzer
    {
        IEnumerable<string> AnalyzeText(string word);
        string DefinePartOfSpeech(string word);
        string GetStandardForm(string text);
    }
}
