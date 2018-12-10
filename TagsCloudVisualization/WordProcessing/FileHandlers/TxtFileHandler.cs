using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace TagsCloudVisualization.WordProcessing.FileHandlers
{
    public class TxtFileHandler : IFileHandler
    {
        public string PathToFile { get; }
        public static readonly Regex Regex = new Regex("^.*\\.(txt)$");

        public TxtFileHandler(string pathToFile)
        {
            PathToFile = pathToFile;
        }
        public IEnumerable<string> ReadFile()
        {
            return File.ReadAllLines(PathToFile, Encoding.Default);
        }
    }
}