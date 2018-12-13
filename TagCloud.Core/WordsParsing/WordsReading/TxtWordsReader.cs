using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace TagCloud.Core.WordsParsing.WordsReading
{
    public class TxtWordsReader : IWordsReader
    {
        public Regex AllowedFileExtension { get; } = new Regex(@"\.txt$", RegexOptions.IgnoreCase);

        public IEnumerable<string> ReadFrom(string path)
        {
            using (var r = new StreamReader(path))
            {
                var curWord = string.Empty;
                int i;
                while ((i = r.Read()) != -1)
                {
                    var c = Convert.ToChar(i);
                    if (char.IsLetterOrDigit(c))
                    {
                        curWord = curWord + c;
                        continue;
                    }
                    if (curWord.Trim() == string.Empty) continue;
                    yield return curWord;
                    curWord = string.Empty;
                }
                if (curWord != string.Empty)
                    yield return curWord;
            }
        }
    }
}