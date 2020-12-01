using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TagsCloudVisualisation.Text
{
    public class FileWordsReader : IFileWordsReader
    {
        private readonly char[] delimiters;
        
        public FileWordsReader(char[] delimiters)
        {
            this.delimiters = delimiters;
        }

        public bool Disposed { get; private set; } = false;

        public IEnumerable<string> EnumerateWordsFrom(string path)
        {
            return EnumerateRawWords(path).Where(x => !string.IsNullOrEmpty(x));
        }

        private IEnumerable<string> EnumerateRawWords(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"File {path} not found", path);

            using var reader = new StreamReader(path);
            
            while (TryReadNextWord(reader, out var word))
                if (word.Length != 0) 
                    yield return word;
        }

        private bool TryReadNextWord(StreamReader reader, out string word)
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
    }
}