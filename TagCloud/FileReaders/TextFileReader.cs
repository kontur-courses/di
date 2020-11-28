using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace TagCloud.FileReaders
{
    public class TextFileReader : IFileReader
    {
        public List<string> ReadWords(string filePath)
        {
            var words = new List<string>();
            using var streamReader = new StreamReader(filePath);
            
            while (!streamReader.EndOfStream)
                words.Add(streamReader.ReadLine());

            return words;
        }
    }
}