using System.Collections.Generic;

namespace TagsCloudContainer.WordsReaders
{
    public interface IWordsReader
    {
        IEnumerable<string> GetWords(string filename);
    }
}