using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer
{
    public class SimpleTextReader : ITextReader
    {
        private readonly string FilePath;
        
        public SimpleTextReader(string path)
        {
            FilePath = path;
        }

        public IEnumerable<string> Read()
        {
            return File.ReadAllLines(FilePath);
        }
    }
}