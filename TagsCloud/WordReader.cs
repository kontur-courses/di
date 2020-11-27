using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace TagsCloud
{
    public static class WordReader
    {
        //Через Regex удобно тестировать разные тексты.
        public static IEnumerable<string> ReadWords(this string filePath)
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