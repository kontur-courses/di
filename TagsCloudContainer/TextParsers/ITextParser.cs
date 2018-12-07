using System.Collections.Generic;

namespace TagsCloudContainer.TextParsers
{
    public interface ITextParser
    {
        List<MiniTag> Parse(string text);
    }
}