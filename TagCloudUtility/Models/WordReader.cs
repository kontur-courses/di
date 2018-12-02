using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TagCloud.Utility.Models
{
    public static class WordReader
    {
        /// <summary>
        /// Read all words from path, splited by regular \W+.
        /// </summary>
        /// <param name="pathToWords"></param>
        /// <returns></returns>
        public static List<string> ReadAllWords(string pathToWords)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), pathToWords);
            var text = new StreamReader(path, Encoding.Default)
                .ReadToEnd();
            return Regex
                .Split(text, @"\W+")
                .Where(w => !string.IsNullOrEmpty(w))
                .ToList();
        }
    }
}