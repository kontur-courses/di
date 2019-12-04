using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloud.FileParsers
{
    public class TxtParser : IFileParser
    {
        public string[] FileExtensions => new string[] { ".txt", ".md" };

        public List<string> Parse(string filename)
        {
            return File.ReadAllLines(filename).Where(l => !string.IsNullOrWhiteSpace(l)).ToList();
        }
    }
}
