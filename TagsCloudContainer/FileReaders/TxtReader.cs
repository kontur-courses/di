using System;
using System.IO;
using TagsCloudContainer.FileOpeners;

namespace TagsCloudContainer.FileReaders
{
    public class TxtReader : IFileReader
    {
        public delegate TxtReader Factory();

        public string[] FileToWordsArray(string filePath)
        {
            return File.ReadAllText(filePath).Split(new[] {Environment.NewLine, " "}, StringSplitOptions.None);
        }
    }
}