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
            return File.ReadAllText(filePath).Trim().Split(new[] {Environment.NewLine, " "}, StringSplitOptions.None);
        }
    }
}