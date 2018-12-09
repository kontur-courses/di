using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace TagsCloudVisualization.WordProcessing.FileHandlers
{
    public class DefaultFileHandler : FileHandler
    {
        public string PathToFile { get; }

        public DefaultFileHandler(string pathToFile)
        {
            PathToFile = pathToFile;
        }
        public IEnumerable<string> ReadFile()
        {
            var result = new List<string>();
            using (var streamReader = new StreamReader(PathToFile))
            {
                while (!streamReader.EndOfStream)
                    result.Add(streamReader.ReadLine());
            }

            return result;
        }
    }
}