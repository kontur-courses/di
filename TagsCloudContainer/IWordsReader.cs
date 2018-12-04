using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IWordsReader
    {
        List<string> ReadWords(string path);
    }
}