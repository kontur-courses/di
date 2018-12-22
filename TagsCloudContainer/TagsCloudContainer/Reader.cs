using System.Collections.Generic;
using System.IO;


namespace TagsCloudContainer
{
    public class Reader : IReader
    {
        private readonly string _filePath;

        public Reader(string filePath)
        {
            _filePath = filePath;
        }

        public IEnumerable<string> ReadWords()
        {
            return File.ReadAllLines(_filePath);
        }
    }
}