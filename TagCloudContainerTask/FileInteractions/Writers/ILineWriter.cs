using System.Collections.Generic;
using System.IO;

namespace FileInteractions.Writers
{
    public interface ILineWriter
    {
        void WriteLinesTo(StreamWriter streamWriter, IEnumerable<string> words);
    }
}