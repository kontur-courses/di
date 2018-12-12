using System;
using System.Collections.Generic;
using System.IO;

namespace TagCloud.Core.TextWorking.WordsReading.WordsReadersForFiles
{
    public class TxtWordsReader : IWordsReaderForFile
    {
        public string ReadingFileExtension { get; } = ".txt";

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