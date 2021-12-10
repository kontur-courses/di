using System.Collections.Generic;

namespace FileInteractions.Readers
{
    public class FromDocReader : ILinesReader
    {
        private readonly string fileName;

        public FromDocReader(string fileName)
        {
            this.fileName = fileName;
        }

        public IEnumerable<string> ReadLines()
        {
        }
    }
}