using System.Collections.Generic;

namespace TagsCloudTextProcessing.Splitters
{
    public interface ITokenizer
    {
        IEnumerable<string> Tokenize(string text);
    }
}