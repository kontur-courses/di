using System.Collections.Generic;

namespace TagsCloudContainer.Interfaces
{
    public interface IWordsReader
    {
        IEnumerable<string> GetWords();
    }
}