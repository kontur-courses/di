using System.Collections.Generic;

namespace TagCloudContainer.Api
{
    public interface IWordsProvider
    {
        IEnumerable<string> GetWords();
    }
}