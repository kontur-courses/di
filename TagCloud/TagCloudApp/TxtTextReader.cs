using System.Collections.Generic;
using System.IO;

namespace TagCloudApp
{
    internal class TxtTextReader : ITextReader
    {
        public bool TryReadWords(string path, out IEnumerable<string> words)
        {
            words = null;
            try
            {
                var text = File.ReadAllText(path);
                words = text.Split(null);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

        public string Extension => ".txt";
    }
}
