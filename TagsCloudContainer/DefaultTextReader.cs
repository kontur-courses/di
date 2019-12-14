using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer
{
    public class DefaultTextReader : ITextReader
    {
        public DefaultTextReader()
        {
        }

        public IEnumerable<string> Read(string path)
        {
            return File.ReadAllLines(path);
        }
    }
}