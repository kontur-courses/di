using System.Collections.Generic;
using System.IO;
using App.Infrastructure.FileInteractions.Writers;

namespace App.Implementation.FileInteractions.Writers
{
    public class LineWriter : ILineWriter
    {
        public void WriteLinesTo(StreamWriter streamWriter, IEnumerable<string> words)
        {
            using (streamWriter)
            {
                foreach (var word in words)
                    streamWriter.WriteLine(word);
            }
        }
    }
}