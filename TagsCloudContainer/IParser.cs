using System.Collections.Generic;

namespace TagsCloudContainer
{
    interface IParser
    {
        IEnumerable<string> GetWords(string text);
    }
}
