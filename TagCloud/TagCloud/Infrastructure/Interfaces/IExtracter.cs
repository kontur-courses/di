using System.Collections.Generic;

namespace TagCloud
{
    public interface IExtracter
    {
        List<WordToken> ExtractWordTokens(string text);
    }
}
