using System.Collections.Generic;

namespace TagsCloudContainer.WordsLoading
{
    public interface IWordsParser
    {
        IEnumerable<string> Parse(string text);
    }
}