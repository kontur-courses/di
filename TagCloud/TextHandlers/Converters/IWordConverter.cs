using System.Collections.Generic;

namespace TagCloud.TextHandlers
{
    public interface IWordConverter
    {
        IEnumerable<string> Convert(IEnumerable<string> words);
        string Convert(string word);
    }
}