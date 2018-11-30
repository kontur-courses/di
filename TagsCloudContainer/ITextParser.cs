using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface ITextParser
    {
        IEnumerable<string> GetWords(string text);
    }
}
