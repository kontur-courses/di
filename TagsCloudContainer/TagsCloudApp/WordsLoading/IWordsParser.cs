using System.Collections.Generic;

namespace TagsCloudApp.WordsLoading
{
    public interface IWordsParser
    {
        IEnumerable<string> Parse(string text);
    }
}