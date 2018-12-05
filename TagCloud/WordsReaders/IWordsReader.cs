using System.Collections.Generic;

namespace TagCloud.WordsReaders
{
    public interface IWordsReader
    {
        List<string> ReadFrom(string path);
    }
}