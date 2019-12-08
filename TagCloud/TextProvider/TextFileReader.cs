using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TagCloud.TextProvider
{
    public class TextFileReader : ITextProvider
    {
        private HashSet<string> FilesPaths { get; set; } = new HashSet<string>
        {
            @"..\..\Input\input.txt",
            @"..\..\Input\song.txt"
        };

        public Encoding TextEncoding { get; set; } = Encoding.UTF8;

        private const string RegexPattern = @"[!, ?._'@\[\] ]+";

        public List<string> GetAllWords()
        {
            var allWordsList = new List<string>();
            foreach (var path in FilesPaths)
                GetAllWordsInOneText(path, allWordsList);
            return allWordsList;
        }

        public List<string> GetAllWords(IEnumerable<string> paths)
        {
            var allWordsList = new List<string>();
            foreach (var path in paths)
                GetAllWordsInOneText(path, allWordsList);
            return allWordsList;
        }

        private void GetAllWordsInOneText(string path, List<string> allWordsList)
        {
            using (var sr = new StreamReader(path, TextEncoding))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                    allWordsList.AddRange(Regex.Split(line, RegexPattern, RegexOptions.IgnoreCase)
                        .Select(s => s.MakeFirstLetterLowerCase()));
            }
        }
    }
}