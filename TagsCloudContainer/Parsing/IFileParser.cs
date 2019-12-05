using System.Collections.Generic;

namespace TagsCloudContainer.Parsing
{
    public interface IFileParser
    {
        IEnumerable<string> ParseFile(string filePath);
    }
}