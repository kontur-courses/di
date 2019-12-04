using System.Collections.Generic;

namespace TagsCloud.FileParsers
{
    public interface IFileParser
    {
        string[] FileExtensions { get; }
        List<string> Parse(string filename);
    }
}
