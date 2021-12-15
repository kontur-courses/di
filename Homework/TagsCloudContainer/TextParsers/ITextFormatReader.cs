using System.Collections.Generic;

namespace TagsCloudContainer.TextParsers
{
    public interface ITextFormatReader
    {
        IEnumerable<string> GetLines(string path);
    }
}
