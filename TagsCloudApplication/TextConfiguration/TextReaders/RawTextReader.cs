using System;
using System.IO;

namespace TextConfiguration.TextReaders
{
    public class RawTextReader : ITextReader
    {
        public string ReadText(string filePath)
        {
            if (filePath is null || !File.Exists(filePath))
                throw new ArgumentException($"Incorrect file path: {filePath}");

            return File.ReadAllText(filePath);
        }
    }
}
