using System.Collections.Generic;
using System.IO;

namespace TagCloud.TextProvider
{
    public class TxtFileReader : ITextProvider
    {
        public HashSet<string> FilesPaths { get; set; } = new HashSet<string>
        {
            @"..\..\..\TagCloud\Input\input.txt",
            @"..\..\..\TagCloud\Input\song.txt"
        };

        public List<string> GetAllLines()
        {
            var allLinesList = new List<string>();
            foreach (var path in FilesPaths)
                GetAllLinesInOneText(path, allLinesList);
            return allLinesList;
        }

        public List<string> GetAllLines(IEnumerable<string> paths)
        {
            var allWordsList = new List<string>();
            foreach (var path in paths)
                GetAllLinesInOneText(path, allWordsList);
            return allWordsList;
        }

        private void GetAllLinesInOneText(string path, List<string> allLinesList)
        {
            {
                var lines = File.ReadAllLines(path);
                allLinesList.AddRange(lines);
            }
        }
    }
}