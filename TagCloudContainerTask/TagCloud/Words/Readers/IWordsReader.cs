using System.Collections.Generic;
using System.IO;

namespace TagCloud.Words.Readers
{
    public interface IWordsReader
    {
        IEnumerable<string> ReadWordsFrom(StreamReader streamReader);
    }
}