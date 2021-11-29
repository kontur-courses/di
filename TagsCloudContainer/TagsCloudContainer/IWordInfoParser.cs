using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IWordInfoParser
    {
        IEnumerable<WordInfo> ParseWords(IEnumerable<string> words);
    }
}