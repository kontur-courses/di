using System.Collections.Generic;

namespace TagsCloudTextProcessing.Tokenizers
{
    public interface ITokenizer
    {
        IEnumerable<Token> Tokenize(IEnumerable<string> words);
    }
}