using System;
using System.Collections.Generic;
using System.IO;

namespace TagCloud.Readers
{
    public class SingleWordInRowTextFileReader : IBoringWordsReader
    {
        private string path;
        private readonly string[] defaultWords = "У меня нет слов".Split();
        public string FileExtFilter => "txt files (*.txt)|*.txt";

        public void SetFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) 
                throw new ArgumentNullException(nameof(path));

            if (!File.Exists(path))
                throw new FileNotFoundException($"file {path} not found");

            this.path = path;
        }

        public IEnumerable<string> ReadWords() =>
            path == null 
                ? defaultWords
                : File.ReadAllLines(path);
    }
}
