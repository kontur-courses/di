using System.IO;
using System.Linq;

namespace TagCloud
{
    public class OneWordInLineParser : IWordParser
    {
        private readonly string[] words;
        public OneWordInLineParser(string inputFileName)
        {
            var workingDirectory = Directory.GetCurrentDirectory();
            var index = workingDirectory.IndexOf("TagCloud");
            var tagCloudPath = workingDirectory.Substring(0, index);
            //TODO: Move to pathfinder
            var path = tagCloudPath + inputFileName;
            words = File.ReadLines(path).ToArray();
        }
        public string[] GetWords()
        {
            //TODO: Add filterWords
            return words;
        }
    }
}