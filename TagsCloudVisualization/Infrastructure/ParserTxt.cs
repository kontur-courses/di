using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TagsCloudVisualization.Infrastructure
{
    public class ParserTxt : IParser
    {
        public string FileType => "txt";

        public IEnumerable<string> WordParse(string path)
        {
            return File.ReadAllLines(path, Encoding.UTF8);
        }
    }
}