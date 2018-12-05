using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagCloud.WordsReaders;

namespace TagCloud.TextReaders
{
    public class TxtWordsReader : IWordsReader
    {
        public List<string> ReadFrom(string path)
        {
            var res = new List<string>();

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
                    res.Add(curWord);
                    curWord = string.Empty;
                }
                if (curWord != string.Empty)
                    res.Add(curWord);
            }

            return res;
        }
    }
}