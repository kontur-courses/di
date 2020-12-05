using System.Collections.Generic;

namespace TagsCloud.FileReaders
{
    interface IFileReader
    {
        public IEnumerable<string> GetWordsFromFile(string filePath);
    }
}