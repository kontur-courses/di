using System;
using System.Collections.Generic;

namespace TagsCloudVisualization.Parsers
{
    public class LinesParser : ITextParser
    {
        public IEnumerable<string> ParseText(string text)
        {
            return text.Split(new string[1]{Environment.NewLine}, StringSplitOptions.None);
        }
    }
}