using System.Collections.Generic;

namespace TagsCloudVisualization.Parsers
{
    public interface ITextParser
    {
        IEnumerable<string> ParseText(string text);
    }
}