using System.Collections.Generic;

namespace TagCloud.TextHandlers.Converters
{
    public interface IWordConverter
    {
        IEnumerable<string> Convert(IEnumerable<string> words);
        string Convert(string word);
    }
}