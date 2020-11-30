using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TagsCloudVisualisation.Text
{
    public class WordsFileReader : IWordsReader
    {
        private readonly char[] delimiters;
        private readonly StreamReader reader;

        public WordsFileReader(string path, char[] delimiters)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"File {path} not found", path);

            this.delimiters = delimiters;
            Path = path;
            reader = new StreamReader(path);
        }

        public string Path { get; }
        public bool Disposed { get; private set; } = false;

        public IEnumerable<string> EnumerateWords()
        {
            while (TryReadNextWord(out var word))
                if (word.Length != 0)
                    yield return word;
        }

        private bool TryReadNextWord(out string word)
        {
            var sb = new StringBuilder();
            if (reader.EndOfStream)
            {
                word = string.Empty;
                return false;
            }

            do
            {
                var symbol = (char) reader.Read();
                if (delimiters.Contains(symbol))
                    break;
                sb.Append(symbol);
            } while (!reader.EndOfStream);

            word = sb.ToString();
            return true;
        }

        public void Dispose()
        {
            if (Disposed)
                throw new InvalidOperationException($"{nameof(WordsFileReader)} for file {Path} is already disposed!");
            reader.Dispose();
            Disposed = true;
        }
    }
}