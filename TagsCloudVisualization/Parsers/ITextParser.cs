using System.Collections.Generic;

namespace TagsCloudVisualization.Parsers
{
    internal interface ITextParser
    {
        IEnumerable<string> ParseText(string text);
    }
}