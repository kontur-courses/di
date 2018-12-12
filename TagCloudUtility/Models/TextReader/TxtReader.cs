using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TagCloud.Utility.Models.TextReader
{
    public class TxtReader : ITextReader
    {
        public string[] ReadToEnd(string pathToWords)
        {
            if (!pathToWords.EndsWith(".txt") && !pathToWords.EndsWith(".ini"))
                throw new ArgumentException($"Wrong format of file {pathToWords}, {nameof(TxtReader)} requires .txt/.ini format");
            if (!File.Exists(pathToWords))
                throw new ArgumentException($"{pathToWords} doesn't exist!");

            var text = File.ReadAllText(pathToWords, Encoding.Default);
            return Regex
                .Split(text, @"\W+")
                .Where(w => !string.IsNullOrEmpty(w))
                .ToArray();
        }
    }
}