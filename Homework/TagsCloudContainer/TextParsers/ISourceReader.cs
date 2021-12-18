using System.Collections.Generic;

namespace TagsCloudContainer.TextParsers
{
    public interface ISourceReader
    {
        IEnumerable<string> GetNextWord();

    }
}
