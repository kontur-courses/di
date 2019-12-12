using System.Collections.Generic;

namespace TagsCloudTextProcessing.Tokenizers
{
    public interface ITokenizer
    {
        IEnumerable<string> Tokenize(string text);
    }
}