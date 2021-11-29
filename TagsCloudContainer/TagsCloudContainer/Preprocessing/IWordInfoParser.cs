using System.Collections.Generic;

namespace TagsCloudContainer.Preprocessing
{
    public interface IWordInfoParser
    {
        IEnumerable<WordInfo> ParseWords(IEnumerable<string> words);
    }
}