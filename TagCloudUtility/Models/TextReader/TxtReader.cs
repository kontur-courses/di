using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TagCloud.Utility.Models.TextReader
{
    public class TxtReader : ITextReader
    {
        /// <summary>
        /// Read all words from path, splited by regular \W+.
        /// </summary>
        /// <param name="pathToWords"></param>
        /// <returns></returns>
        public string[] ReadToEnd(string pathToWords)
        {
            if (!pathToWords.EndsWith(".txt"))
                throw new InvalidOperationException($"Wrong format of file {pathToWords}, {this} requires .txt format");
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