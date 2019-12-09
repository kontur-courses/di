using System.Collections.Generic;
using TagsCloudVisualization.Structures;

namespace TagsCloudVisualization.WordAnalyzers
{
    public interface IMorphAnalyzer
    {
        IEnumerable<WordInfo> AnalyzeText(string text);
        string DefinePartOfSpeech(string word);
        string GetStandardForm(string word);
    }
}
