using System.Collections.Generic;
using System.IO;

namespace TagCloud.Words.Writers
{
    public class LineWriter : ILineWriter
    {
        public void WriteLinesTo(StreamWriter streamWriter, IEnumerable<string> words)
        {
            using (streamWriter)
            {
                streamWriter.WriteLine(words);
            }
        }
    }
}