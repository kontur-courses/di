using System.IO;
using System;

namespace TagCloud.FileReader
{
    public class TxtFileReader : IFileReader
    {
        public string ReadAllText(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File {filePath} doesn't exist");

            var text = File.ReadAllText(filePath);

            if (text.Length == 0)
                throw new Exception($"File {filePath} is empty");

            return text;
        }
    }
}
