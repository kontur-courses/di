using System.Collections.Generic;

namespace TagsCloudContainer.Input
{
    public interface IWordParser
    {
        IEnumerable<string> ParseWords(string input);
    }
}