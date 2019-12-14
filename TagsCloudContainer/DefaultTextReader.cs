using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer
{
    public class DefaultTextReader : ITextReader
    {
        //private readonly string FilePath;
        
        public DefaultTextReader()
        {
            //FilePath = path;
        }

        public IEnumerable<string> Read(string path)
        {
            return File.ReadAllLines(path);
        }
    }
}