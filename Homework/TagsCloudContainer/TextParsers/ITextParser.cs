using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface ITextParser
    {
        IReadOnlyDictionary<string, int> GetWordsCounts();
    }
}
