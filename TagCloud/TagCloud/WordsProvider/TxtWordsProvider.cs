using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace TagCloud.WordsProvider
{
    public class TxtWordsProvider : FileWordsProvider
    {
        public TxtWordsProvider(string filePath) : base(filePath)
        {
        }

        protected override bool CheckFile(string filePath)
        {
            return filePath.EndsWith(".txt");
        }

        public override IEnumerable<string> GetWords()
        {
            var words = Regex.Split(File.ReadAllText(FilePath), @"\W+");
            return words;
        }
    }
}