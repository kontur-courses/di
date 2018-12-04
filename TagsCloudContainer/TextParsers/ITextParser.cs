using System.Collections.Generic;

namespace TagsCloudContainer.TextParsers
{
    public interface ITextParser
    {
        List<(string, int)> Parse();
    }
}