using System;
using System.Collections.Generic;
using System.IO;

namespace TagCloud.Readers
{
    public class SingleWordInRowTextFileReader : IReader, IBoringWordsReader
    {
        private string path;

        public string FileExtFilter => "txt files (*.txt)|*.txt";

        public void Open(string path)
        {
            if (path == null) 
                throw new ArgumentNullException(nameof(path));

            if (!File.Exists(path))
                throw new FileNotFoundException($"file {path} not found");

            this.path = path;
        }

        public IEnumerable<string> ReadWords() =>
            path == null 
                ? "У меня нет слов".Split()
                : File.ReadAllLines(path);
    }
}
