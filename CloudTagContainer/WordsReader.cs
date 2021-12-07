using System;
using System.IO;

namespace CloudTagContainer
{
    public class WordsReader
    {
        public string[] Read(Stream inputStream)
        {
            using var streamReader = new StreamReader(inputStream);
            var allString = streamReader.ReadToEnd();
            return allString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }
    }
}