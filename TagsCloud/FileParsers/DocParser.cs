using System.Collections.Generic;

namespace TagsCloud.FileParsers
{
    public class DocParser //: IFileParser
    {
        public string[] FileExtensions => new string[] { ".doc", ".docx" };

        public List<string> Parse(string filename)
        {
            return new List<string> { "tag1", "tag1", "tag1", "tag2", "tag2", "tag3" };
        }
    }
}
