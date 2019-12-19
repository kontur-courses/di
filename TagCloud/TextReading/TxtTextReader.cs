using System.Collections.Generic;
using System.IO;

namespace TagCloud.TextReading
{
    public class TxtTextReader : ITextReader
    {
        public IEnumerable<string> ReadWords(FileInfo file)
        {
            try
            {
                return File.ReadLines(file.FullName);
            }
            catch (IOException e)
            {
                throw new IOException($"File {file.FullName} is in use", e);
            }
            
        }

        public string Extension => ".txt";
    }
}
