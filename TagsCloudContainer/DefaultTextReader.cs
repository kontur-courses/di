using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer
{
    public class DefaultTextReader : ITextReader
    {
        private readonly string FilePath;
        
        public DefaultTextReader(string path)
        {
            FilePath = path;
        }

        public IEnumerable<string> Read()
        {
            return File.ReadAllLines(FilePath);
        }
    }
}