using System.Collections.Generic;

namespace TagsCloudContainer.TextParsers
{
    public interface ITextParser
    {
        IReadOnlyDictionary<string, int> GetWordsCounts();
    }
}
