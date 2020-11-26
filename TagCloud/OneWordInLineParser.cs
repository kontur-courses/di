using System.IO;
using System.Linq;

namespace TagCloud
{
    public class OneWordInLineParser : IWordParser
    {
        private readonly string[] words;
        public OneWordInLineParser(string inputFileName)
        {
            words = File.ReadLines(inputFileName).ToArray();
        }
        public string[] GetWords()
        {
            return words;
        }
    }
}