using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace TagCloud
{
    public class TxtWordsProvider : WordsProvider
    {
        public readonly string filePath;

        public TxtWordsProvider(string filePath)
        {
            if (!filePath.EndsWith(".txt"))
                throw new ArgumentException("Not a .txt file");
            this.filePath = filePath;
        }

        public override IEnumerable<string> GetWords()
        {
            var words = Regex.Split(File.ReadAllText(filePath), @"\W+");
            return words;
        }
    }
}