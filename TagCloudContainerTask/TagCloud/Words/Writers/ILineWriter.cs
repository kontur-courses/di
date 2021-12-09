using System.Collections.Generic;
using System.IO;

namespace TagCloud.Words.Writers
{
    public interface ILineWriter
    {
        void WriteLinesTo(StreamWriter streamWriter, IEnumerable<string> words);
    }
}