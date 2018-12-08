using System;
using System.Collections.Generic;

namespace TagCloudApp
{
    internal class CsvTextReader : ITextReader
    {
        public bool TryReadWords(string path, out IEnumerable<string> words) => throw new NotImplementedException();

        public string Extension => ".csv";
    }
}
