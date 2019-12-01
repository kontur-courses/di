using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer.FileReading
{
    public class OneWordInLineFileReader : IFileReader
    {
        public IEnumerable<string> ReadWords(string textFileName)
        {
            return File.ReadLines(textFileName);
        }
    }
}