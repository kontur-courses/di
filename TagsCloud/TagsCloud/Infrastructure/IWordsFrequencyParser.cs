using System.Collections.Generic;

namespace TagsCloud
{
    public interface IWordsFrequencyParser
    {
        Dictionary<string, int> ParseWordsFrequency(string fileName);
    }
}