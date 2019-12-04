using System.Collections.Generic;

namespace TagCloud
{
    public interface IWordsHandler
    {
        Dictionary<string, int> GetWordsAndCount(string path);
        Dictionary<string, int> Conversion(Dictionary<string, int> wordsAndCount);
    }
}