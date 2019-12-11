using System.Collections.Generic;

namespace TagsCloudContainer.Data.Readers
{
    public interface IWordsFileReader
    {
        IEnumerable<string> ReadAllWords(string path);
    }
}