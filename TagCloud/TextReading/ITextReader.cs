using System.Collections.Generic;
using System.IO;

namespace TagCloud.TextReading
{
    public interface ITextReader
    { 
        IEnumerable<string> ReadWords(FileInfo file);
        string Extension { get; }
    }
}
