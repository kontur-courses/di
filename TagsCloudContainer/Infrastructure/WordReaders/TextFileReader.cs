using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Infrastructure
{
    internal class TextFileReader : IWordReader
    {
        private const string fileExtension = ".txt";

        public bool TryReadWords(string filename, out string[] words)
        {
            words = Array.Empty<string>();

            if (!File.Exists(filename) || Path.GetExtension(filename) != fileExtension)
                return false;

            var wordList = new List<string>();
            using var stream = new StreamReader(filename);
            while (!stream.EndOfStream)
                wordList.Add(stream.ReadLine()!);

            words = wordList.ToArray();
            return true;
        }
    }
}