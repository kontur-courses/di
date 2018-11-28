using System.Collections.Generic;

namespace TagsCloudContainer
{
    interface ITextParser
    {
        IEnumerable<string> GetWords(string text);
    }
}
