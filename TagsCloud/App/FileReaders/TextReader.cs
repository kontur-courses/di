using System.Collections.Generic;
using System.IO;
using TagsCloud.Infrastructure;

namespace TagsCloud.App.FileReaders
{
    public class TextReader : IFileAllLinesReader
    {
        public HashSet<string> Extensions { get; } = new HashSet<string> {".txt", ".md"};

        public string[] ReadAllLines(string filePath)
        {
            return File.ReadAllLines(filePath);
        }
    }
}