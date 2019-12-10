using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TagCloud.TextProvider
{
    public class TextFileReader : ITextProvider
    {
        public HashSet<string> FilesPaths { get; set; } = new HashSet<string>
        {
            @"..\..\Input\input.txt",
            @"..\..\Input\song.txt"
        };

        public Encoding TextEncoding { get; set; } = Encoding.UTF8;

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