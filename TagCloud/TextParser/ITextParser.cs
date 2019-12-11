using System.Collections.Generic;

namespace TagCloud.TextParser
{
    public interface ITextParser
    {
        List<string> ParseText();
        List<string> ParseText(IEnumerable<string> paths);
    }
}