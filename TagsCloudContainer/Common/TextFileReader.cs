using System.Collections.Generic;
using System.IO;
using TagsCloudContainer.Common.Contracts;

namespace TagsCloudContainer.Common
{
    public class TextFileReader : IFileReader
    {
        public string[] SupportedFormats => new[] {"txt"};

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