using System.IO;
using System.Linq;

namespace TagCloud
{
    public class OneWordInLineParser : IWordParser
    {
        private IPathCreater creater;
        public OneWordInLineParser(IPathCreater creater)
        {
            this.creater = creater;
        }
        
        public string[] GetWords(string inputFileName)
        {
            return WordProcessing(File.ReadLines(creater.GetPathToFile(inputFileName)).ToArray());
        }

        private string[] WordProcessing(string[] words)
        {
            //TODO: Add filterWords
            return words.Select(str => str.ToLower()).ToArray();
        }
    }
}