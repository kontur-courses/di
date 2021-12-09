using System.Collections.Generic;
using System.IO;

namespace TagCloud.Words.Reading
{
    public interface IWordsReader
    {
        IEnumerable<string> ReadWordsFrom(StreamReader streamReader);
    }
}