using System.Collections.Generic;
using System.IO;

namespace TagsCloudVisualization.Common.FileReaders
{
    public class TextFileReader : IFileReader
    {
        public IEnumerable<string> ReadLines(string path)
        {
            using var reader = new StreamReader(path);
            while (!reader.EndOfStream)
                yield return reader.ReadLine();
        }
    }
}