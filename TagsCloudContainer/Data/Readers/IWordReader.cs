using System.Collections.Generic;

namespace TagsCloudContainer.Data.Readers
{
    public interface IWordReader
    {
        IEnumerable<string> ReadAllWords();
    }
}