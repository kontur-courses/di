using System.Collections.Generic;

namespace TagsCloudApp.WordsLoading
{
    public interface IWordsProvider
    {
        IEnumerable<string> GetWords();
    }
}