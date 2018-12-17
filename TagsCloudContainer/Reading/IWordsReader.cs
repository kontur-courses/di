using System.Collections.Generic;

namespace TagsCloudContainer.Reading
{
    public interface IWordsReader
    {
        List<string> ReadWords(string inputPath);
    }
}