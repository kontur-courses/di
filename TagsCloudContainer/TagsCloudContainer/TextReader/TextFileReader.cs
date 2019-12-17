using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer
{
    public class TextFileReader : ITextReader
    {
        private readonly string fileName;

        public TextFileReader(string fileName)
        {
            this.fileName = fileName;
        }

        public IEnumerable<string> GetLines()
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException(string.Format("Text file {0} is not found", fileName));
            return File.ReadLines(fileName);
        }
    }
}