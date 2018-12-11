using System.Collections.Generic;

namespace TagsCloudContainer.TextParsers
{
    public interface ITextParser
    {
        List<WordFrequency> Parse(string text);
    }
}