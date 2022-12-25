using System;
using System.IO;

namespace TagsCloudContainer.FileReaders
{
    public class TxtReader : IFileReader
    {
        public string[] FileToWordsArray(string filePath)
        {
            if (!File.Exists(filePath))
                throw new ArgumentException("File doesn't exist");
            var result = File.ReadAllText(filePath);
            if (result.Length == 0)
                throw new ArgumentException("Empty file");
            return result.Trim().Split(new[] {Environment.NewLine, " "}, StringSplitOptions.None);
        }
    }
}