using System.Collections.Generic;
using TagsCloudVisualization.Logic;

namespace TagsCloudVisualization.Services
{
    public interface IParser
    {
        IEnumerable<WordToken> ParseToTokens(string text);
    }
}