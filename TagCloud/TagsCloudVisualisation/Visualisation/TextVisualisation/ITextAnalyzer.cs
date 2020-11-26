using System.Collections.Generic;

namespace TagsCloudVisualisation.Visualisation.TextVisualisation
{
    public interface ITextAnalyzer
    {
        void RegisterText(string text);
        IEnumerable<string> GetSortedWords();
    }
}