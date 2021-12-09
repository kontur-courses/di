using System.Collections.Generic;
using System.IO;

namespace TagCloud.Words.Readers
{
    public interface ILinesReader
    {
        IEnumerable<string> ReadLinesFrom(StreamReader streamReader);
    }
}