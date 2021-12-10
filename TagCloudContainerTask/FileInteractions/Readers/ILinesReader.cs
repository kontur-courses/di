using System.Collections.Generic;
using System.IO;

namespace FileInteractions.Readers
{
    public interface ILinesReader
    {
        IEnumerable<string> ReadLinesFrom(StreamReader streamReader);
    }
}