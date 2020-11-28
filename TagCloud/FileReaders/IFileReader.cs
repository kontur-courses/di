using System.Collections.Generic;    

namespace TagCloud.FileReaders
{
    public interface IFileReader
    {
        public List<string> ReadWords(string filePath);
    }
}
