using System.Collections.Generic;

namespace TagCloud
{
    public interface ITextParcer
    {
        List<string> TryGetWordsFromText();
        Dictionary<string, int> ParseWords(List<string> words);
    }
}