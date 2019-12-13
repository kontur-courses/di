using System.Collections.Generic;

namespace TagsCloudContainer.WordProcessing
{
    public interface IWordProvider
    {
        IEnumerable<string> GetWords();
    }
}