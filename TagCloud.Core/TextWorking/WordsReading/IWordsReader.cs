using System.Collections.Generic;

namespace TagCloud.Core.TextWorking.WordsReading
{
    public interface IWordsReader
    {
        IEnumerable<string> ReadFrom(string path);
    }
}