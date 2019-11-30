using System.Collections.Generic;

namespace TagsCloudContainer.TextParsing
{
    public interface IFileWordsParser
    {
        IEnumerable<string> ParseFrom(string path);
    }
}