using System.Collections.Generic;

namespace TagsCloudContainer.Reader
{
    public interface IReader
    {
        IEnumerable<string> GetWordsSet(string path);
    }
}