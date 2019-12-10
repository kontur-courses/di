using System.Collections.Generic;

namespace TagCloud.TextProvider
{
    public interface ITextParser
    {
        List<string> ParseText();
        List<string> ParseText(IEnumerable<string> paths);
    }
}