using System.Collections.Generic;

namespace TagsCloudContainer.WordsLoading
{
    public interface IWordsLoader
    {
        IEnumerable<string> LoadWords(string fileName);
    }
}