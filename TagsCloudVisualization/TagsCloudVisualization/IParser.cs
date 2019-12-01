using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface IParser
    {
        IEnumerable<WordToken> ParseToTokens(string text);
    }
}