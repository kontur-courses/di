using System.Collections.Generic;

namespace TagCloud.TextProvider
{
    public interface ITextProvider
    {
        Dictionary<string, int> GetParsedText();
    }
}