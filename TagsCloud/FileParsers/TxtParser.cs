using System.Collections.Generic;
using System.IO;

namespace TagsCloud.FileParsers
{
    public class TxtParser : IFileParser
    {
        public string[] FileExtensions => new string[] { ".txt", ".md" };

        public List<string> Parse(string filename)
        {
            var res = new List<string>();
            var separators = new char[] { ' ', ',', '.', '!', '?', '(', ')', '{', '}', '[', ']' };
            foreach (var line in File.ReadAllLines(filename))
            {
                foreach (var word in line.Split(separators, System.StringSplitOptions.RemoveEmptyEntries))
                    res.Add(word);
            }
            return res;
        }
    }
}
