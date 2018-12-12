using System.Collections.Generic;

namespace TagsCloudContainer.WordsHandlers
{
    public interface IWordsHandler
    {
        IEnumerable<WordInfo> HandleWords(IEnumerable<string> words);
    }
}