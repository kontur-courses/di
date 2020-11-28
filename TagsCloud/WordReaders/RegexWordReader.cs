using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TagsCloud.WordReaders;

namespace TagsCloud
{
    public class RegexWordReader : IWordReader
    {
        private readonly string filePath;

        public RegexWordReader(string filePath)
        {
            this.filePath = filePath;
        }

        //Через Regex удобно тестировать разные тексты.
        public IEnumerable<string> ReadWords()
        {
            try
            {
                using var reader = new StreamReader(filePath);
                return Regex.Split(reader.ReadToEnd(), "\\W+");
            }
            catch
            {
                Console.WriteLine($"Failed to read {filePath}");
                return null;
            }
        }
    }
}