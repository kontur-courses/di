using System.Collections.Generic;
using System.IO;

namespace TagCloudApp
{
    internal class NewLineTextReader : ITextReader
    {
        public string Extension => ".txt";

        public bool TryReadWords(string path, out IEnumerable<string> words)
        {
            words = null;
            try
            {
                words = File.ReadAllLines(path);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }
    }
}
