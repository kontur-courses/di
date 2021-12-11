using System.Collections.Generic;
using System.IO;

namespace App.Infrastructure.FileInteractions.Writers
{
    public interface ILineWriter
    {
        void WriteLinesTo(StreamWriter streamWriter, IEnumerable<string> words);
    }
}