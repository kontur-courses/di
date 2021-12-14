using System.Collections.Generic;
using System.IO;

namespace TagsCloudVisualization.Common.FileReaders
{
    public class TextFileReader : IFileReader
    {
        public string ReadFile(string path)
        {
            return File.ReadAllText(path);
        }

        public IEnumerable<string> ReadLines(string path)
        {
            using var reader = new StreamReader(path);
            while (!reader.EndOfStream)
                yield return reader.ReadLine();
        }
    }
}