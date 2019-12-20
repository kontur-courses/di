using System.Collections.Generic;

namespace TagsCloudContainer.Reader
{
    public interface IReaderLinesFromFile
    {
        IEnumerable<string> GetWordsSet(string path);
    }
}